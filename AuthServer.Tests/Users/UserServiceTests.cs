using AuthServer.Repositories.Users;
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
        [InlineData("yourmom")]
        [InlineData("your@mom@.web")]
        [InlineData("@#!@$(@(#)$(#)&&*()!@gmail.com")]
        public void CreateUser_ValidatesEmail(string email)
        {
            var service = CreateService();
           
            Assert.Throws<ArgumentException>(() => service.CreateUser(email));
        }

        [Theory]
        [InlineData("jim", "johnson", "jim.johnson@gmail.com")]
        public void CreateUser_Succeeds(string first, string last, string email)
        {
            var service = CreateService();
        }


        private IUserService CreateService()
        {
            return new UserService(new UserRepository());
        }

    }
}
