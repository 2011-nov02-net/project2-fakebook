using Fakebook.Domain.Repository;
using Fakebook.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Fakebook.RestApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fakebook.UnitTests.Repository.Post
{
    public class PostRepository_ReadTests
    { 
        /// <summary>
        /// test the results of what happens in Index Display Users
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Index_Display_UsersAsync()
        {
            // ARRANGE

            var mockRepository = new Mock<IUserRepo>();

            // create a moq that returns users
            mockRepository.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(GetDatabaseSession());

            // make a controller using my mock
            var controller = new UserController(mockRepository.Object);

            // ACT
            var result = await controller.Get();

            // assert
            var viewResult = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void Index_Display_Users()
        {
            // ARRANGE

            var mockRepository = new Mock<IUserRepo>();

            // create a moq that returns users
            mockRepository.Setup(r => r.GetAllUsers()).Returns(GetDatabaseSession);

            // make a controller using my mock
            var controller = new UserController(mockRepository.Object);
            
            // ACT
            var result = controller.Get();

            // assert
            var viewResult = Assert.IsAssignableFrom<IActionResult>(result.Result);
        }
        [Fact]
        public void Index_Get_User_By_ID()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepo>();
            var p = GetDatabaseSession();
            // create a moq that returns users
            mockRepository.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync(p.First);

            // make a controller using my Mock
            var controller = new UserController(mockRepository.Object);
            // ACT
            // get the "get by user ids function"
            var result = controller.Get(1);

            // test if there is something in the result and test to see if it's an IActionResult
            Assert.NotNull(result);
            var viewResult = Assert.IsAssignableFrom<IActionResult>(result.Result);

        }

        /// <summary>
        /// Standard list of users to be added.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Domain.User> GetDatabaseSession()
        {
            var person1 = new Domain.User();
            person1.Id = 1;
            person1.FirstName = "Jordan";
            person1.LastName = "Garcia";
            person1.ProfilePictureUrl = "NULL";

            var person2 = new Domain.User();
            person2.Id = 2;
            person2.FirstName = "Jay";
            person2.ProfilePictureUrl = "NULL";

            List<Domain.User> users = new List<Domain.User>();

            users.Add(person1);
            users.Add(person2);

            return users;

        }
    }
}
