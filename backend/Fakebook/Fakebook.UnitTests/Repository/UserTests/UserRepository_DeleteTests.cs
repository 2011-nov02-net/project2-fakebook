using System;

using Fakebook.UnitTests.TestData;

using Xunit;

namespace Fakebook.UnitTests.Repository.UserTests
{
    public class UserRepository_DeleteTests
    {
        [Theory]
        [ClassData(typeof(UserTestData.Read.Valid))]
        public void DeleteUser_ValidData(int userId) {

        }

        [Theory]
        [ClassData(typeof(UserTestData.Read.Invalid))]
        public void DeleteUser_InvalidData(int userId) {

        }
    }
}
