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



    }
}
