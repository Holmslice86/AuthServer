using System.Collections.Generic;
using AuthServer.Users.Users;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Host.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserCollection _userCollection;

        public UserController(IUserCollection userCollection)
        {
            _userCollection = userCollection;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userCollection.GetUsers();
        }

        [HttpGet("{userId}")]
        public User Get(string userId)
        {
            return _userCollection.GetUser(userId);
        }

        [HttpPost]
        public void Post([FromBody]string email, string password)
        {
            _userCollection.AddUser(email, password);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        [HttpDelete("{userId}")]
        public void Delete(string userId)
        {
            _userCollection.RemoveUser(userId);
        }
    }
}
