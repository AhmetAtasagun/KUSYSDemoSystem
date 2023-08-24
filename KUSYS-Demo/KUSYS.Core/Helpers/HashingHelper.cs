using KUSYS.Core.Models.Authorization;
using System.Security.Cryptography;

namespace KUSYS.Business.Helpers
{
    public class HashingHelper
    {
        public static string GenerateSecurityCode()
        {
            var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }

        public static string HashUse(string password, string salt)
        {
            using var bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt));
            var randomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(randomKey);
            return hash;
        }
        public static bool CheckPassword(UserAuth user, string password)
        {
            return user.Password == HashUse(password, user.PasswordSalt);
        }
    }
}
