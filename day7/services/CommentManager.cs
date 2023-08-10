using Microsoft.EntityFrameworkCore;
using blogCRUDWithEFCore.Model;
using postgresDb.Data;

namespace blog.Services
{
    public class CommentManager
    {
        private readonly ApiDbContext _dbContext;

        public CommentManager(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Comment>> GetAllCommentsAsync(int postId)
        {
            try
            {
                var post = await _dbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.PostId == postId);

                if (post == null)
                {
                    return new List<Comment>();
                }

                return post.Comments.ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching comments.", ex);
            }
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            try
            {
                var specificComment = await _dbContext.Comments.SingleOrDefaultAsync(c => c.CommentId == commentId);

                if (specificComment == null)
                {
                    return null;
                }

                return specificComment;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching comments.", ex);
            }
        }

        public async Task<bool> CreateCommentAsync(Comment comment, int postId)
        {
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
                if (post != null)
                {
                    post.Comments.Add(comment);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or take appropriate action
                throw new ApplicationException("An error occurred while creating comment.", ex);
            }
        }

        public async Task UpdateCommentAsync(Comment updatedComment)
        {
            try
            {
                _dbContext.Comments.Update(updatedComment);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or take appropriate action
                throw new ApplicationException("An error occurred while updating comment.", ex);
            }
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);
            try
            {
                if (comment == null)
                {
                    return false;
                }
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting comment.", ex);
            }
        }
    }
}
