using System;

namespace AuthServer.Users.Users
{
    public class User
    {
        public User(string email)
        {
            SubjectId = Guid.NewGuid().ToString();
            ValidateEmail(email);
            Email = email;
        }

        public string SubjectId { get; }

        public string Email { get; }

        private void ValidateEmail(string email)
        {
            var emailValidator = new EmailValidator();

            if (!emailValidator.IsValidEmail(email))
                throw new ArgumentException("Invalid Email Address");
        }
    }

}
