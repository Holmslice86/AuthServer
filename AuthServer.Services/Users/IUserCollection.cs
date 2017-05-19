using System.Collections.Generic;

namespace AuthServer.Users.Users
{
    public interface IUserCollection
    {
        User GetUser(string subjectId);

        IEnumerable<User> GetUsers();

        void AddUser(string email);

        void RemoveUser();
    }
}
