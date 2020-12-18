using System;
using System.Threading.Tasks;

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
        [Fact]
        public async Task Create_ValidComment_Inserts() {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookContext>()
                .UseSqlite(connection)
                .Options;

            
            var user = new User
            {
                Id = 0
            };

            var comment = new Comment
            {
                User = user
            };

            bool result;

            // Act
            using (var actingContext = new FakebookContext(options)) {
                actingContext.Database.EnsureCreated();

                var repo = new CommentRepo(actingContext);

                result = await repo.CreateAsync(comment);
            }

            Assert.True(result);

            // Assert
            using var assertionContext = new FakebookContext(options);
            /*
            using var context2 = new SimpleOrderContext(options);
            LocationEntity locationActual = context2.Locations
                .Include(l => l.Orders)
                .Single(l => l.Name == "abc");
            Assert.Equal(location.Stock, locationActual.Stock);
            Assert.Empty(locationActual.Orders);
             */
        }

        public CommentRepository_CreateTests() {



        }
    }
}
