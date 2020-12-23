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
            CommentRepository_CreateTests.CreateComments(options, comments);

            using (var assertionContext = new FakebookContext(options)) {
                var commentRepo = new CommentRepo(assertionContext);

                var commentExpected = comments[commentId - 1];
                var commentActual = commentRepo.GetCommentByIdAsync(commentId).Result;

                Assert.NotNull(commentActual);
                Assert.Equal(commentExpected.Content, commentActual.Content);
            }
        }

        [Theory]
        [ClassData(typeof(CommentTestData.ReadById.Invalid))]
        public void GetComment_ById_InvalidData(List<Comment> comments, int commentId) {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            CommentRepository_CreateTests.CreateComments(options, comments);

            using (var assertionContext = new FakebookContext(options)) {
                var commentRepo = new CommentRepo(assertionContext);

                Assert.ThrowsAsync<ArgumentException>(() => commentRepo.GetCommentByIdAsync(commentId));
            }
        }
    
        [Theory]
        [ClassData(typeof(CommentTestData.ReadByIds.Valid))]
        public void GetComments_ByIds_ValidData(List<Comment> comments, List<int> commentIds) {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            CommentRepository_CreateTests.CreateComments(options, comments);
            
            // Assert
            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var commentsActual = repo.GetAllUsers();

                Assert.True(commentsActual.Any());

                commentsActual = repo.GetUsersByIdsAsync(commentIds).Result;

                Assert.True(commentsActual.Count() == commentIds.Distinct().Count());
            }
        }   

        [Theory]
        [ClassData(typeof(CommentTestData.ReadByIds.Invalid))]
        public void GetComments_ByIds_InvalidData(List<Comment> comments, List<int> commentIds) {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            CommentRepository_CreateTests.CreateComments(options, comments);

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new CommentRepo(assertionContext);

                Assert.ThrowsAsync<ArgumentException>(() => repo.GetCommentsByIdsAsync(commentIds));
            }
        }

        [Theory]
        [ClassData(typeof(CommentTestData.ReadByUserId.Valid))]
        public void GetComments_ByUserId_ValidData(List<Comment> comments, int userId, int expectedCount) {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                          .UseSqlite(connection)
                          .Options;

            // Act
            CommentRepository_CreateTests.CreateComments(options, comments);

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new CommentRepo(assertionContext);

                var commentsActual = repo.GetCommentsByUserIdAsync(userId).Result;
                Assert.Equal(commentsActual.Count(), expectedCount);
            }
        }

        [Theory]
        [ClassData(typeof(CommentTestData.ReadByUserId.Invalid))]
        public void GetComments_ByUserId_InvalidData(List<Comment> comments, int userId) {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                          .UseSqlite(connection)
                          .Options;

            // Act
            CommentRepository_CreateTests.CreateComments(options, comments);

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new CommentRepo(assertionContext);

                Assert.ThrowsAsync<ArgumentException>(() => repo.GetCommentsByUserIdAsync(userId));
            }
        }
        
        public void GetComments_ByPostId_ValidData(List<Comment> comments, int postId, int expectedCount) {
        }

        public void GetComments_ByPostId_InvalidData(List<Comment> comments, int postId) {

        }
    }
}
