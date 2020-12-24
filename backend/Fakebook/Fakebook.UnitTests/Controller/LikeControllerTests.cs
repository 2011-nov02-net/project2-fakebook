using System;
using System.Collections.Generic;

using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Fakebook.RestApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace Fakebook.UnitTests.Controller
{
    public class LikeControllerTests
    {
        public IEnumerable<Post> GetPostSession() {
            return new List<Post>
            {

            };
        }

        //[Theory]
        //[InlineData(1, 1)]
        //public void LikePost_Valid(int postId, int userId) {
        //    // arrange
        //    var mockedPostRepo = new Mock<IPostRepo>();
        //    var mockedUserRepo = new Mock<IUserRepo>();

        //    mockedPostRepo
        //        .Setup(mpr => mpr.LikePostAsync(It.IsAny<int>(), It.IsAny<int>()))
        //        .ReturnsAsync(true);

        //    var controller = new LikeController(mockedPostRepo.Object, mockedUserRepo.Object);

        //    // act
        //    var result = controller
        //        .Like(postId)
        //        .Result;

        //    //assert
        //    var viewResult = Assert.IsAssignableFrom<IActionResult>(result);

        //    var likesCount = mockedPostRepo
        //        .Object
        //        .CountLikesAsync(postId)
        //        .Result;
        //}

        //public void LikePost_Invalid() {

        //}

        //public void UnlikePost_Valid() {

        //}

        //public void UnlikePost_Invalid() {

        //}
    }
}
