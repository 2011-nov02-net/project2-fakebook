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

namespace Fakebook.UnitTests.Repository.UserTests
{
    public class UserRepository_DeleteTests
    {
        [Theory]
        [ClassData(typeof(UserTestData.ReadById.Valid))]
        public void DeleteUser_ValidData(List<User> users, int userId) {
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
                users.ForEach(user => _ = repo.CreateUser(user).Result);
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var usersActual = repo.GetAllUsers();

                Assert.True(usersActual.Any());

                bool userActual = repo.DeleteUserAsync(userId).Result;

                Assert.True(userActual);
            }
        }

        [Theory]
        [ClassData(typeof(UserTestData.ReadById.Invalid))]
        public void DeleteUser_InvalidData(List<User> users, int userId) {
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
                users.ForEach(user => _ = repo.CreateUser(user).Result);
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var usersActual = repo.GetAllUsers();

                Assert.ThrowsAsync<ArgumentException>(() => repo.DeleteUserAsync(userId));
            }
        }
    }
}
