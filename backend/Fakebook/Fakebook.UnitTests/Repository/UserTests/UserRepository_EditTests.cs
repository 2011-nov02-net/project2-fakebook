using System;

using Fakebook.DataAccess.Model;
using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Fakebook.UnitTests.TestData;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.UnitTests.Repository.UserTests
{
    public class UserRepository_EditTests
    {
        [Theory]
        [ClassData(typeof(UserTestData.Update.Valid))]
        public void UpdateUser_ValidData(User user, User userUpdates) {
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

                var repo = new UserRepo(actingContext);

                // Create the user data
                result = repo.CreateUserAsync(user).Result;
            }

            // Assert
            Assert.True(result != -1, "Unable to create the user.");

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                bool updateResult = repo.UpdateUserAsync(user.Id, userUpdates).Result;

                Assert.True(updateResult, "Unable to update the user.");
                var alteredUser = repo.GetUserByIdAsync(user.Id).Result;

                Assert.NotEqual(user.FirstName, alteredUser.FirstName);
                Assert.NotEqual(user.LastName, alteredUser.LastName);
                Assert.NotEqual(user.Status, alteredUser.Status);
            }
        }

        [Theory]
        [ClassData(typeof(UserTestData.Update.Invalid))]
        public void UpdateUser_InvalidData(User user, User userUpdates) {
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

                var repo = new UserRepo(actingContext);

                // Create the user data
                result = repo.CreateUserAsync(user).Result;
            }

            // Assert
            Assert.True(result != -1, "Unable to create the user.");

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                Assert.ThrowsAsync<ArgumentException>(() => repo.UpdateUserAsync(user.Id, userUpdates));

                var userActual = repo.GetUserByIdAsync(user.Id).Result;

                Assert.Equal(user.FirstName, userActual.FirstName);
                Assert.Equal(user.LastName, userActual.LastName);
                Assert.Equal(user.Email, userActual.Email);
            }
        }
    }
}
