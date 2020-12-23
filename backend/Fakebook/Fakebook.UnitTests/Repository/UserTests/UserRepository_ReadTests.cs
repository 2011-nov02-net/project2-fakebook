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
        [ClassData(typeof(UserTestData.ReadById.Valid))]
        public void GetUser_ById_ValidData(List<User> users, int userId) {
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
                users.ForEach(user => _ = repo.CreateUserAsync(user).Result);
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
        [ClassData(typeof(UserTestData.ReadById.Invalid))]
        public void GetUser_ById_InvalidData(List<User> users, int userId) {
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
                users.ForEach(user => _ = repo.CreateUserAsync(user).Result);
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var usersActual = repo.GetAllUsers();

                Assert.ThrowsAsync<ArgumentException>(() => repo.GetUserByIdAsync(userId));
            }
        }

        [Theory]
        [ClassData(typeof(UserTestData.ReadByEmail.Valid))]
        public void GetUser_ByEmail_ValidData(List<User> users, string userEmail) {
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
                users.ForEach(user => _ = repo.CreateUserAsync(user).Result);
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var usersActual = repo.GetAllUsers();

                Assert.True(usersActual.Any());

                var userActual = repo.GetUserByEmailAsync(userEmail).Result;

                Assert.NotNull(userActual);

                Assert.Equal(userActual.FirstName, userActual.FirstName);
                Assert.Equal(userActual.LastName, userActual.LastName);
                Assert.Equal(userActual.BirthDate, userActual.BirthDate);
            }
        }

        [Theory]
        [ClassData(typeof(UserTestData.ReadByEmail.Invalid))]
        public void GetUser_ByEmail_InvalidData(List<User> users, string userEmail) {
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
                users.ForEach(user => _ = repo.CreateUserAsync(user).Result);
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var usersActual = repo.GetAllUsers();

                Assert.ThrowsAsync<ArgumentException>(() => repo.GetUserByEmailAsync(userEmail));
            }
        }

        [Theory]
        [ClassData(typeof(UserTestData.ReadByIds.Valid))]
        public void GetUser_ByIds_ValidData(List<User> users, List<int> userIds) {
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
                users.ForEach(user => _ = repo.CreateUserAsync(user).Result);
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var usersActual = repo.GetAllUsers();

                Assert.True(usersActual.Any());

                usersActual = repo.GetUsersByIdsAsync(userIds).Result;

                Assert.True(usersActual.Any());
                Assert.True(usersActual.Count() == userIds.Distinct().Count());
            }
        }

        [Theory]
        [ClassData(typeof(UserTestData.ReadByIds.Invalid))]
        public void GetUser_ByIds_InvalidData(List<User> users, List<int> userIds) {
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
                users.ForEach(user => _ = repo.CreateUserAsync(user).Result);
            }

            using (var assertionContext = new FakebookContext(options)) {
                var repo = new UserRepo(assertionContext);

                var usersActual = repo.GetAllUsers();

                Assert.ThrowsAsync<ArgumentException>(() => repo.GetUsersByIdsAsync(userIds));
            }
        }
    }
}
