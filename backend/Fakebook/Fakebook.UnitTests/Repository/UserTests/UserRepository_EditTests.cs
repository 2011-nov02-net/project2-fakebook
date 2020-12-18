using System;

using Fakebook.Domain;
using Fakebook.UnitTests.TestData;

using Xunit;

namespace Fakebook.UnitTests.Repository.UserTests
{
    public class UserRepository_EditTests
    {
        [Theory]
        [ClassData(typeof(UserTestData.Update.Valid))]
        public void UpdateUser_ValidData(User user, User userUpdates) {

        }

        [Theory]
        [ClassData(typeof(UserTestData.Update.Invalid))]
        public void UpdateUser_InvalidData(User user, User userUpdates) {

        }
    }
}
