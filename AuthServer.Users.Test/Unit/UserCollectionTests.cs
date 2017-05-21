using System;
using AuthServer.Users.Users;
using Xunit;

namespace AuthServer.Users.Test.Unit
{
    public class UserCollectionTests
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
            var collection = CreateCollection();
            var password = "ABC123!@";
            Assert.Throws<ArgumentException>(() => collection.AddUser(email, password));
        }

        [Theory]
        [InlineData("jim", "johnson", "jim.johnson@gmail.com")]
        public void CreateUser_Succeeds(string first, string last, string email)
        {
            var collection = CreateCollection();
        }


        private IUserCollection CreateCollection()
        {
            return new UserCollection();
        }

    }
}
