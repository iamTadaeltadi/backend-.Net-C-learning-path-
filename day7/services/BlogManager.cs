using Microsoft.EntityFrameworkCore;
using blogCRUDWithEFCore.Model;
using postgresDb.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blog.Services
{
    public class BlogManager
    {
        private readonly ApiDbContext _dbContext;

        public BlogManager(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            try
            {
                return await _dbContext.Posts.Include(p => p.Comments).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log if needed, and return an appropriate response
                throw new ApplicationException("An error occurred while fetching posts.", ex);
            }
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            try
            {
                return await _dbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.PostId == postId);
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log if needed, and return an appropriate response
                throw new ApplicationException("An error occurred while fetching post.", ex);
            }
        }

        public async Task AddPostAsync(Post post)
        {
            try
            {
                _dbContext.Posts.Add(post);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log if needed, and throw an appropriate exception
                throw new ApplicationException("An error occurred while creating post.", ex);
            }
        }

        public async Task UpdatePostAsync(Post updatedPost)
        {
            try
            {
                _dbContext.Posts.Update(updatedPost);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log if needed, and throw an appropriate exception
                throw new ApplicationException("An error occurred while updating post.", ex);
            }
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
                if (post != null)
                {
                    _dbContext.Posts.Remove(post);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log if needed, and throw an appropriate exception
                throw new ApplicationException("An error occurred while deleting post.", ex);
            }
        }
        
    }
}
