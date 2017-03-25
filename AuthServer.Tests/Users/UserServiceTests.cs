using AuthServer.Services.Users;
using System;
using Xunit;

namespace AuthServer.Tests.Users
{
    public class UserServiceTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateUser_ValidatesFirstName(string firstName)
        {
            Assert.Throws<ArgumentException>(() => CreateUser(firstName, "Johnson", "jim@gmail.com"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateUser_ValidatesLastName(string lastName)
        {
            Assert.Throws<ArgumentException>(() => CreateUser("Jim", lastName, "jim@gmail.com"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateUser_ValidatesEmail(string email)
        {
            Assert.Throws<ArgumentException>(() => CreateUser("Jim", "Johnson", email));
        }


        private User CreateUser(string first, string last, string email)
        {
            return new User(first, last, email);
        }

    }
}
