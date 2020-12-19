using System;
using System.Linq;

using Fakebook.DataAccess.Model;
using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Fakebook.UnitTests.TestData;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Fakebook.UnitTests.Repository.UserTests
{
    public class UserRepository_CreateTests
    {
        [Theory]
        [ClassData(typeof(UserTestData.Create.Valid))]
        public void CreateUser_ValidData(User user) {
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
                result = repo.CreateUser(user).Result;
            }

            // Assert
            Assert.True(result != -1, "Unable to create the user.");

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var users = repo.GetAllUsers();

                Assert.True(users.Any());

                var userActual = repo.GetUserByIdAsync(result).Result;

                Assert.NotNull(userActual);

                Assert.Equal(user.Email, userActual.Email);
                Assert.Equal(user.FirstName, userActual.FirstName);
                Assert.Equal(user.LastName, userActual.LastName);
                Assert.Equal(user.BirthDate, userActual.BirthDate);
            }
        }

        [Theory]
        [ClassData(typeof(UserTestData.Create.Invalid))]
        public void CreateUser_InvalidData(User user) {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                .UseSqlite(connection)
                .Options;

            // Act
            using (var actingContext = new FakebookContext(options)) {
                actingContext.Database.EnsureCreated();

                var repo = new UserRepo(actingContext);

                // Create the user data
                Assert.ThrowsAsync<ArgumentException>(() => repo.CreateUser(user));
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var users = repo.GetAllUsers();

                Assert.False(users.Any());
            }
        }
    }
}
