using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AuthServer.Users.Infrastructure
{
    public class Cryptography
    {
        public static string Hash(string value, string salt)
        {
            var saltedValue = Encoding.UTF8.GetBytes(value + salt);
            var hashedValue = SHA256.Create().ComputeHash(saltedValue);
            return Encoding.UTF8.GetString(hashedValue);
        }

        public static bool ConfirmPassword(string password, string storedHash, string storedSalt)
        {
            var passwordHash = Encoding.UTF8.GetBytes(Hash(password, storedSalt));
            var storedhashByes = Encoding.UTF8.GetBytes(storedHash);
            return storedhashByes.SequenceEqual(passwordHash);
        }
    }
}
