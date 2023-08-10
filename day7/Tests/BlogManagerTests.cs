using blog.Services;
using blogCRUDWithEFCore.Model;
using Microsoft.EntityFrameworkCore;
using postgresDb.Data;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public async void AddPostToDatabase()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDatabase")
               .Options;

        using (var dbContext = new ApiDbContext(options))
        {
            var blogManager = new BlogManager(dbContext);
            var post = new Post
            {
                Title = "Test Post",
                Content = "Test Content",
                
            };

            // Act
            await blogManager.AddPostAsync(post);

            // Assert
            var addedPost = dbContext.Posts.FirstOrDefault();
            Assert.NotNull(addedPost);
            Assert.Equal("Test Post", addedPost.Title);
            Assert.Equal("Test Content", addedPost.Content);

    }
}

    [Fact]
    public async void GetPostById_PostExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApiDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

        // Create a new DbContext to add a post to the database
        using (var dbContext = new ApiDbContext(options))
        {
            dbContext.Database.EnsureDeleted();

            var blogManager = new BlogManager(dbContext);
            var post = new Post
            {
                Title = "Test Post",
                Content = "Test Content",
                CreatedAt = DateTime.UtcNow
            };

            dbContext.Posts.Add(post);
            dbContext.SaveChanges();
        }

        // Act
        using (var dbContext = new ApiDbContext(options))
        {
            var blogManager = new BlogManager(dbContext);

            var retrievedPost = await  blogManager.GetPostByIdAsync(1); 

            // Assert
            Assert.NotNull(retrievedPost);
            Assert.Equal("Test Post", retrievedPost.Title);
            Assert.Equal("Test Content", retrievedPost.Content);
            Assert.Equal(DateTime.UtcNow.Date, retrievedPost.CreatedAt.Date);
        }
    }
    [Fact]
    public async void GetPostById_PostDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
          .UseInMemoryDatabase(databaseName: "TestDatabase")
          .Options;

       

        // Act
        using (var dbContext = new ApiDbContext(options))
        {
            var blogManager = new BlogManager(dbContext);

            var retrievedPost = await blogManager.GetPostByIdAsync(1000);//assume this id has not been created yet

            // Assert
            Assert.Null(retrievedPost);
            
        }
    }

    [Fact]
    public async void GetAllPosts_NoPosts()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

        // Act
        using (var dbContext = new ApiDbContext(options))
        {
            dbContext.Database.EnsureDeleted();//delete existing posts
            var blogManager = new BlogManager(dbContext);

            var posts = await  blogManager.GetAllPostsAsync();

            // Assert
            Assert.Empty(posts); // Ensure that the returned list is empty
        }
    }

    [Fact]
    public async  void GetAllPosts_WithPosts()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

        using (var dbContext = new ApiDbContext(options))
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var blogManager = new BlogManager(dbContext);

            var post1 = new Post
            {
                Title = "Test Post 1",
                Content = "Test Content 1",
                CreatedAt = new DateTime(2023, 8, 10, 15, 30, 0)
            };

            var post2 = new Post
            {
                Title = "Test Post 2",
                Content = "Test Content 2",
                CreatedAt = new DateTime(2023, 8, 11, 12, 0, 0)
            };

            dbContext.Posts.Add(post1);
            dbContext.Posts.Add(post2);
            dbContext.SaveChanges();

            // Act
            List<Post> posts = await blogManager.GetAllPostsAsync();

            // Assert
            Assert.Equal(2, posts.Count);
            Assert.Contains(posts, p => p.Title == "Test Post 1");
            Assert.Contains(posts, p => p.Title == "Test Post 2");
        }
    }


    [Fact]
    public async void UpdatePost_PostExists()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
    .UseInMemoryDatabase(databaseName: "TestDatabase")
    .Options;

        using (var dbContext = new ApiDbContext(options))
        {
            dbContext.Database.EnsureDeleted();//delete existing posts
            var blogManager = new BlogManager(dbContext);

            // Add a post
            var post = new Post
            {
                Title = "Test Post",
                Content = "Test Content",
                CreatedAt = new DateTime(2023, 8, 10, 12, 0, 0)
            };
            

            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            // Update the post
            var updatedPost = new Post
            {
                PostId = 1, 
                Title = "Updated Title",
                Content = "Updated Content",
                CreatedAt = new DateTime(2023, 8, 11, 12, 0, 0)
            };
            await blogManager.UpdatePostAsync(updatedPost);

            var retrievedPost = dbContext.Posts.FirstOrDefault();

            // Assert
            Assert.NotNull(retrievedPost);
            Assert.Equal("Updated Title", retrievedPost.Title);
            Assert.Equal("Updated Content", retrievedPost.Content);

        }



    }
    [Fact]
    public async void DeletePost_PostExists_ShouldReturnTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApiDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

        using (var dbContext = new ApiDbContext(options))
        {
            var post = new Post
            {
                Title = "Test Post",
                Content = "Test Content",
                CreatedAt = DateTime.UtcNow
            };
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            var blogManager = new BlogManager(dbContext);

            // Act
            var result =  await blogManager.DeletePostAsync(post.PostId);

            // Assert
            Assert.True(result);
            
        }


       
    }


    public async void DeletePost_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApiDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

        using (var dbContext = new ApiDbContext(options))
        {
            var blogManager = new BlogManager(dbContext);

            // Act
            var result = await blogManager.DeletePostAsync(1); 

            // Assert
            Assert.False(result);
        }
    }
}

