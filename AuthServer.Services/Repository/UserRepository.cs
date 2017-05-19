using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AuthServer.Users.Users;
using Dapper;

namespace AuthServer.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection)
        {
            _connection = connection;
        }


        public User GetUser(string subjectId)
        {


            throw new NotImplementedException();
        }

        public IList<User> GetUsers()
        {
            _connection.Open();

            const string sql = "";

            var results = _connection.Query<UserDataModel>(sql)
                                     .Select(x => new User(x.Email))
                                     .ToList();
            _connection.Close();

            return results;
        }

        public void CreateUser(User user)
        {
            _connection.Open();

            const string sql = "";

            _connection.Execute(sql, new { user.SubjectId, user.Email });

            _connection.Close();
        }

        public void DeleteUser(string subjectId)
        {
            throw new NotImplementedException();
        }
    }
}
