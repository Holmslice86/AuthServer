using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(string subjectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void CreateUser(string email)
        {
            var user = new User(email);
        }

        public void DeleteUser()
        {
            throw new NotImplementedException();
        }

    }
}
