using System;

namespace AuthServer.Users.Users
{
    public class User
    {
        public User(string email, string password)
        {
            UserId = Guid.NewGuid().ToString();
            ValidateEmail(email);
            Email = email;
            Password = password;
        }

        public string UserId { get; }

        public string Email { get; }

        public string Password { get; }

        private void ValidateEmail(string email)
        {
            var emailValidator = new EmailValidator();

            if (!emailValidator.IsValidEmail(email))
                throw new ArgumentException("Invalid Email Address");
        }
    }

}
