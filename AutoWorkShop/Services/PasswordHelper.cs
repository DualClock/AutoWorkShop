using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoWorkshop.Services
{
    public static class PasswordHelper
    {



        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var salt = "AutoWorkShop_Salt_2024";
                var saltedPassword = password + salt;
                var bytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }




        public static bool VerifyPassword(string password, string hash)
        {
            var computedHash = HashPassword(password);
            return computedHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
