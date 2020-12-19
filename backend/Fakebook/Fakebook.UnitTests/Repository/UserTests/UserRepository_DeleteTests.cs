using System.Collections.Generic;

using Fakebook.Domain;
using Fakebook.UnitTests.TestData;

using Xunit;

namespace Fakebook.UnitTests.Repository.UserTests
{
    public class UserRepository_DeleteTests
    {
        [Theory]
        [ClassData(typeof(UserTestData.Delete.Valid))]
        public void DeleteUser_ValidData(List<User> users, int userId) {

        }

        [Theory]
        [ClassData(typeof(UserTestData.Delete.Invalid))]
        public void DeleteUser_InvalidData(List<User> users, int userId) {

        }
    }
}
