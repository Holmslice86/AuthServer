using System;
using System.Collections.Generic;

namespace AuthServer.Users.Users
{
    public class UserCollection : IUserCollection
    {

        public UserCollection()
        {
        }

        public User GetUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void AddUser(string email, string password)
        {
            var user = new User(email, password);
        }

        public void RemoveUser(string userId)
        {
            throw new NotImplementedException();
        }

    }
}
