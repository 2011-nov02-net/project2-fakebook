using System;
using System.Collections.Generic;
using System.Linq;

using Fakebook.DataAccess.Model;
using Fakebook.Domain;
using Fakebook.Domain.Repository;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.UnitTests.Repository.CommentTests
{
    public class CommentRepository_CreateTests
    {
        public static void CreateComments(DbContextOptions<FakebookContext> options, List<Comment> comments) {
            using (var actingContext = new FakebookContext(options)) {
                actingContext.Database.EnsureCreated();

                var postRepo = new PostRepo(actingContext);
                var commentRepo = new CommentRepo(actingContext);

                // Create the user data
                comments.ForEach(comment => {
                    int result = postRepo.CreatePostAsync(comment.Post).Result;
                    Assert.NotEqual(-1, result);

                    var post = postRepo.GetPostByIdAsync(result).Result;
                    comment.User = post.User;
                    comment.Post = post;

                    result = commentRepo.CreateAsync(comment).Result;
                    Assert.NotEqual(-1, result);
                });
            }
        }
        
        [Theory]
        [ClassData(typeof(CommentTestData.Create.Valid))]
        public void CreateComment_ValidData(Comment comment) {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                .UseSqlite(connection)
                .Options;

            int result;
            
            // Act
            using (var actingContext = new FakebookContext(options)) {
                actingContext.Database.EnsureCreated();

                var userRepo = new UserRepo(actingContext);
                var postRepo = new PostRepo(actingContext);
                var commentRepo = new CommentRepo(actingContext);

                result = postRepo.CreatePostAsync(comment.Post).Result;

                Assert.True(userRepo.GetAllUsersAsync().Result.Any());
                Assert.True(postRepo.GetAllPostsAsync().Result.Any());

                // Create the user data
                result = commentRepo.CreateAsync(comment).Result;
            }

            // Assert
            Assert.True(result != -1, "Unable to create the comment.");

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new CommentRepo(assertionContext);

                var comments = repo.GetAllAsync().Result;

                Assert.True(comments.Any());

                var commentActual = repo.GetCommentByIdAsync(result).Result;

                Assert.NotNull(commentActual);

                Assert.Equal(comment.Content, commentActual.Content);
            }
        }

        [Theory]
        [ClassData(typeof(CommentTestData.Create.Invalid))]
        public void CreateComment_InvalidData(Comment comment) {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            using (var actingContext = new FakebookContext(options)) {
                actingContext.Database.EnsureCreated();

                var userRepo = new UserRepo(actingContext);
                var postRepo = new PostRepo(actingContext);
                var commentRepo = new CommentRepo(actingContext);
                try {
                    _ = userRepo.CreateUserAsync(comment.User).Result;
                } catch {
                    // to skip the user not being created
                }

                try {
                    _ = postRepo.CreatePostAsync(comment.Post).Result;
                } catch {
                    // to skip the post not being created/null
                }

                // Create the user data
                Assert.ThrowsAsync<ArgumentException>(() => commentRepo.CreateAsync(comment));
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new CommentRepo(assertionContext);

                var comments = repo.GetAllAsync().Result;

                Assert.True(!comments.Any());
            }
        }
    }
}
