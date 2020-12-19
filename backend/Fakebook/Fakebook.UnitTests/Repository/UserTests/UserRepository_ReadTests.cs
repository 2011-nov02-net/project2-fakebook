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
    public class UserRepository_ReadTests
    {
        [Theory]
        [ClassData(typeof(UserTestData.Read.Valid))]
        public void GetUser_ValidData(List<User> users, int userId) {
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

                var userActual = repo.GetUserByIdAsync(userId).Result;

                Assert.NotNull(userActual);

                Assert.Equal(userActual.Email, userActual.Email);
                Assert.Equal(userActual.FirstName, userActual.FirstName);
                Assert.Equal(userActual.LastName, userActual.LastName);
                Assert.Equal(userActual.BirthDate, userActual.BirthDate);
            }
        }

        [Theory]
        [ClassData(typeof(UserTestData.Read.Invalid))]
        public void GetUser_InvalidData(List<User> users, int userId) {
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

                Assert.ThrowsAsync<ArgumentException>(() => repo.GetUserByIdAsync(userId));
            }
        }
    }
}
