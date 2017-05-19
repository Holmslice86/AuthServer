using System;
using System.Collections.Generic;

namespace AuthServer.Users.Users
{
    public class UserCollection : IUserCollection
    {

        public UserCollection()
        {
        }

        public User GetUser(string subjectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void AddUser(string email)
        {
            var user = new User(email);
        }

        public void RemoveUser()
        {
            throw new NotImplementedException();
        }

    }
}
