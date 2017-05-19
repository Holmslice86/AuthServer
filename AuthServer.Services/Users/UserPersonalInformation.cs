using System;

namespace AuthServer.Users.Users
{
    public class UserPersonalInformation
    {

        public UserPersonalInformation(string firstName, string lastName)
        {
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private void ValidateFirstName(string first)
        {
            if (string.IsNullOrWhiteSpace(first))
                throw new ArgumentException("Invalid User First Name");
        }

        private void ValidateLastName(string last)
        {
            if (string.IsNullOrWhiteSpace(last))
                throw new ArgumentException("Invalid User Last Name");
        }

    }
}
