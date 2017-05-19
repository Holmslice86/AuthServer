using System.Collections;
using System.Collections.Generic;

namespace AuthServer.Users.Users
{
    public interface IUserRepository
    {
        User GetUser(string subjectId);
        IList<User> GetUsers();
        void CreateUser(User user);
        void DeleteUser(string subjectId);
    }
}
