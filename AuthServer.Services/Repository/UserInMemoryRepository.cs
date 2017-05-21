using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthServer.Users.Users;

namespace AuthServer.Users.Repository
{
    public class UserInMemoryRepository : IUserRepository
    {
        private List<User> _users;

        public UserInMemoryRepository()
        {
            _users = new List<User>();
        }

        public User GetUser(string userId)
        {
            return _users.FirstOrDefault(x => x.UserId == userId);
        }

        public IList<User> GetUsers()
        {
            return _users;
        }

        public void CreateUser(User user)
        {
            _users.Add(user);
        }

        public void DeleteUser(string userId)
        {
            _users.RemoveAll(x => x.UserId == userId); 
        }
    }
}
