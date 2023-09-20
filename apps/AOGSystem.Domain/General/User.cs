using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.General
{
    public class User : BaseEntity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public void SetUsername(string username) { Username = username; }
        public void SetFirstName(string firstName) { FirstName = firstName; }
        public void SetLastName(string lastName) { LastName = lastName; }
        public void SetEmail(string email) { Email = email; }
        public void SetPhoneNumber(string phoneNumber) { PhoneNumber = phoneNumber; }
        public void SetPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt
            using (var hasher = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashedPassword = hasher.GetBytes(32); // 32 bytes is the size of a SHA-256 hash
                byte[] hashedWithSalt = new byte[hashedPassword.Length + salt.Length];
                Array.Copy(hashedPassword, hashedWithSalt, hashedPassword.Length);
                Array.Copy(salt, 0, hashedWithSalt, hashedPassword.Length, salt.Length);
                Password = Convert.ToBase64String(hashedWithSalt);
            }
        }

        public bool VerifyPassword(string password)
        {
            byte[] hashedWithSalt = Convert.FromBase64String(Password);
            byte[] salt = new byte[16];
            Array.Copy(hashedWithSalt, hashedWithSalt.Length - 16, salt, 0, 16);

            using (var hasher = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] newHashedPassword = hasher.GetBytes(32);
                for (int i = 0; i < 32; i++)
                {
                    if (newHashedPassword[i] != hashedWithSalt[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
