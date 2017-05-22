using System.Collections;
using System.Collections.Generic;

namespace AuthServer.Users.Users
{
    public interface IUserRepository
    {
        User GetUser(string userId);
        User GetUserByEmailWithPassword(string email);
        IList<User> GetUsers();
        void CreateUser(string email, string password);
        void DeleteUser(string userId);
    }
}
