using Microsoft.EntityFrameworkCore;
using blog.Services;
using blogCRUDWithEFCore.Model;
using postgresDb.Data;

namespace Tests
{
    public class CommentManagerTests
    {
        [Fact]
        public async void GetAllComments_PostExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApiDbContext(options))
            {
                dbContext.Database.EnsureDeleted();
                var post = new Post
                {
                    Title = "Test Post",
                    Content = "Test Content",
                    CreatedAt = new DateTime(2023, 8, 11, 12, 0, 0)
                };

                var comment1 = new Comment { CommentId = 2, PostId = 1, Text = "comments 1", CreatedAt = DateTime.UtcNow };
                var comment2 = new Comment { CommentId = 1, PostId =1, Text ="comments 2", CreatedAt = DateTime.UtcNow };
                post.Comments.Add(comment1);
                post.Comments.Add(comment2);
                dbContext.Posts.Add(post);
                dbContext.SaveChanges();

                var commentManager = new CommentManager(dbContext);

                // Act
                var comments= await commentManager.GetAllCommentsAsync(1);

                // Assert
                Assert.NotNull(comments);
                Assert.Equal(2, comments.Count);
                Assert.Contains(comments, c => c.Text == "comments 1");
                Assert.Contains(comments, c => c.Text == "comments 2");
            }
        }

        [Fact]
        public async void GetAllComments_PostDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApiDbContext(options))
            {
                var commentManager = new CommentManager(dbContext);

                // Act
                var comments = await commentManager.GetAllCommentsAsync(123); // Assuming post ID doesn't exist

                // Assert
                Assert.NotNull(comments);
                Assert.Empty(comments);
            }
        }


        [Fact]
        public async void GetCommentById_CommentExists()
        {
                // Arrange
                var options = new DbContextOptionsBuilder<ApiDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

                using (var dbContext = new ApiDbContext(options))
                {
                    dbContext.Database.EnsureDeleted();
                    var post = new Post
                    {
                        Title = "Test Post",
                        Content = "Test Content",
                        CreatedAt = new DateTime(2023, 8, 11, 12, 0, 0)
                    };

                    var comment1 = new Comment { CommentId = 2, PostId = 1, Text = "comments 1", CreatedAt = DateTime.UtcNow };
                    var comment2 = new Comment { CommentId = 1, PostId = 1, Text = "comments 2", CreatedAt = DateTime.UtcNow };
                    post.Comments.Add(comment1);
                    post.Comments.Add(comment2);
                    dbContext.Posts.Add(post);
                    dbContext.SaveChanges();

                    var commentManager = new CommentManager(dbContext);

                // Act
                var comment = await commentManager.GetCommentByIdAsync(1); 

                // Assert
                Assert.NotNull(comment);
            }
            }

            [Fact]
            public async void GetCommentByIdCommentPostsDoesNotExsits(){

            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApiDbContext(options))
            {
                dbContext.Database.EnsureDeleted();
               

                var commentManager = new CommentManager(dbContext);

                // Act
                var comment = await  commentManager.GetCommentByIdAsync(124);//assume this id does not exists

                // Assert
                Assert.Null(comment);
            }


        }

        [Fact]
        public async void CreateCommentAsync_ValidData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApiDbContext(options))
            {
                dbContext.Database.EnsureDeleted();

                var post = new Post
                {
                    Title = "Test Post",
                    Content = "Test Content",
                    CreatedAt = new DateTime(2023, 8, 11, 12, 0, 0)
                };
                dbContext.Posts.Add(post);
                dbContext.SaveChanges();

                var commentManager = new CommentManager(dbContext);

                var newComment = new Comment
                {
                    Text = "New comment",
                    CreatedAt = DateTime.UtcNow
                };

                // Act
                var result = await commentManager.CreateCommentAsync(newComment, post.PostId);

                // Assert
                Assert.True(result);

                var postWithComment = await dbContext.Posts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.PostId == post.PostId);

                Assert.NotNull(postWithComment);
                Assert.Single(postWithComment.Comments);
                Assert.Equal("New comment", postWithComment.Comments.First().Text);
            }
        }

        [Fact]
        public async void UpdateCommentAsync_ValidData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApiDbContext(options))
            {
                dbContext.Database.EnsureDeleted();

                var comment = new Comment { CommentId = 1, PostId = 1, Text = "Initial comment", CreatedAt = DateTime.UtcNow };
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();

                var commentManager = new CommentManager(dbContext);

                var updatedComment = new Comment
                {
                    CommentId = comment.CommentId,
                    PostId = comment.PostId,
                    Text = "Updated comment",
                    CreatedAt = comment.CreatedAt
                };

                // Act
                await commentManager.UpdateCommentAsync(updatedComment);

                // Assert
                var updatedCommentFromDb = await dbContext.Comments.FirstOrDefaultAsync(c => c.CommentId == comment.CommentId);
                Assert.NotNull(updatedCommentFromDb);
                Assert.Equal("Updated comment", updatedCommentFromDb.Text);
            }
        }

        [Fact]
        public async void DeleteCommentAsync_ValidData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApiDbContext(options))
            {
                dbContext.Database.EnsureDeleted();

                var comment = new Comment { CommentId = 1, PostId = 1, Text = "To be deleted", CreatedAt = DateTime.UtcNow };
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();

                var commentManager = new CommentManager(dbContext);

                // Act
                var result = await commentManager.DeleteCommentAsync(comment.CommentId);

                // Assert
                Assert.True(result);

                var deletedCommentFromDb = await dbContext.Comments.FirstOrDefaultAsync(c => c.CommentId == comment.CommentId);
                Assert.Null(deletedCommentFromDb);
            }
        }




    }
}

