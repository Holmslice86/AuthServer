using System.Collections.Generic;
using AuthServer.Users.Users;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Host.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepository.GetUsers();
        }

        [HttpGet("{userId}")]
        public User Get(string userId)
        {
            return _userRepository.GetUser(userId);
        }

        [HttpPost]
        public void Post([FromBody]string email, string password)
        {
            _userRepository.CreateUser(email, password);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        [HttpDelete("{userId}")]
        public void Delete(string userId)
        {
            _userRepository.DeleteUser(userId);
        }
    }
}
