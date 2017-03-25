using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AuthServer.Services.Users
{
    public class User
    {
        public User(string firstName, string lastName, string email)
        {
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidateEmail(email);


            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

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

        private void ValidateEmail(string email)
        {
            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid Email Address");
        }

        public bool IsValidEmail(string strIn)
        {
            var invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
