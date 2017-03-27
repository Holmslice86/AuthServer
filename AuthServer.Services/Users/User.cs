using System;

namespace AuthServer.Services.Users
{
    public class User
    {
        public User(string email)
        {
            ValidateEmail(email);
            Email = email;
        }

        public string Email { get; set; }

        private void ValidateEmail(string email)
        {
            var emailValidator = new EmailValidator();

            if (!emailValidator.IsValidEmail(email))
                throw new ArgumentException("Invalid Email Address");
        }
    }

}
