using System;
using AuthServer.Users.Users;
using Xunit;

namespace AuthServer.Users.Test.Unit
{
    public class UserTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("yourmom")]
        [InlineData("your@mom@.web")]
        [InlineData("@#!@$(@(#)$(#)&&*()!@gmail.com")]
        public void CreateUser_ValidatesEmail(string email)
        {
            var password = "ABC123!@";
            Assert.Throws<ArgumentException>(() => User.CreateUser(email, password));
        }

        [Theory]
        [InlineData("jim", "johnson", "jim.johnson@gmail.com")]
        public void CreateUser_Succeeds(string first, string last, string email)
        {
            var password = "ABC123!@";
            var user = User.CreateUser(email, password);
            Assert.True(user != null);
        }

    }
}
