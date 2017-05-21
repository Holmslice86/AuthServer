using System.Collections.Generic;

namespace AuthServer.Users.Users
{
    public interface IUserCollection
    {
        User GetUser(string userId);

        IEnumerable<User> GetUsers();

        void AddUser(string email, string password);

        void RemoveUser(string userId);
    }
}
