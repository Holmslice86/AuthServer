using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthServer.Users.Users;

namespace AuthServer.Users.Repository
{
    public class UserInMemoryRepository : IUserRepository
    {
        private readonly List<User> _users;

        public UserInMemoryRepository()
        {
            _users = new List<User> { User.CreateUser("alice@gmail.com", "password"), User.CreateUser("bob@gmail.com", "password") };
        }

        public User GetUser(string userId)
        {
            return _users.FirstOrDefault(x => x.UserId == userId);
        }

        public User GetUserByEmailWithPassword(string email)
        {
            return _users.FirstOrDefault(x => x.Email == email);
        }

        public IList<User> GetUsers()
        {
            return _users;
        }

        public void CreateUser(string email, string password)
        {
            var user = User.CreateUser(email, password);
            _users.Add(user);
        }

        public void DeleteUser(string userId)
        {
            _users.RemoveAll(x => x.UserId == userId);
        }
    }
}
