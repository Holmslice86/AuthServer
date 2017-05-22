using System;
using AuthServer.Users.Infrastructure;

namespace AuthServer.Users.Users
{
    public class User
    {
        private User(string userId, string email, string password, string salt)
        {
            UserId = userId;
            Email = email;
            Password = password;
            Salt = salt;
        }

        public string UserId { get; }

        public string Email { get; }

        public string Password { get; }

        public string Salt { get; }

        private static void ValidateEmail(string email)
        {
            var emailValidator = new EmailValidator();

            if (!emailValidator.IsValidEmail(email))
                throw new ArgumentException("Invalid Email Address");
        }

        public static User CreateUser(string email, string password)
        {
            ValidateEmail(email);
            var salt = Guid.NewGuid().ToString();
            var hashedPassword = Cryptography.Hash(password, salt);
            return new User(Guid.NewGuid().ToString(), email, hashedPassword, salt);
        }

        public static User HydratePrivateUser(string userId, string email, string password, string salt)
        {
            return new User(userId, email, password, salt);
        }

        public static User HydratePublicUser(string userId, string email)
        {
            return new User(userId, email, "[REDACTED]", "[REDACTED]");
        }
    }

}
