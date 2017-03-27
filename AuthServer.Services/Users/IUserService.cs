using System.Collections.Generic;

namespace AuthServer.Services.Users
{
    public interface IUserService
    {
        User GetUser(string subjectId);

        IEnumerable<User> GetUsers();

        void CreateUser(string email);

        void DeleteUser();
    }
}
