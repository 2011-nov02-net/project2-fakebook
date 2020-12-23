using System;
using System.Collections.Generic;
using System.Linq;

using Fakebook.DataAccess.Model;
using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Fakebook.UnitTests.TestData;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.UnitTests.Repository.CommentTests
{
    public class CommentRepository_ReadTests
    {
        [Theory]
        [ClassData(typeof(CommentTestData.ReadById.Valid))]
        public void GetComment_ById_ValidData(List<Comment> comments, int commentId) {
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

                // Create the user data
                comments.ForEach(comment => {
                    int result;

                    result = postRepo.CreatePostAsync(comment.Post).Result;
                    Assert.NotEqual(-1, result);

                    var post = postRepo.GetPostByIdAsync(result).Result;
                    comment.User = post.User;
                    comment.Post = post;

                    result = commentRepo.CreateAsync(comment).Result;
                    Assert.NotEqual(-1, result);
                });

                Assert.True(postRepo.GetAllPostsAsync().Result.Any());
                Assert.True(commentRepo.GetAllAsync().Result.Any());
            }

            using (var assertionContext = new FakebookContext(options)) {
                var commentRepo = new CommentRepo(assertionContext);

                var commentExpected = comments[commentId - 1];
                var commentActual = commentRepo.GetCommentByIdAsync(commentId).Result;

                Assert.NotNull(commentActual);
                Assert.Equal(commentExpected.Content, commentActual.Content);
            }
        }
    }
}
