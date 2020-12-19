using System;

using Fakebook.Domain;
using Fakebook.UnitTests.TestData;

using Xunit;

namespace Fakebook.UnitTests.Repository.UserTests
{
    public class UserRepository_ReadTests
    {
        [Theory]
        [ClassData(typeof(UserTestData.Read.Valid))]
        public void GetUser_ValidData(int userId) {

        }

        [Theory]
        [ClassData(typeof(UserTestData.Read.Invalid))]
        public void GetUser_InvalidData(int userId) {

        }
    }
}
