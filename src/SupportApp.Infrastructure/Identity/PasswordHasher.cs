using System.Security.Cryptography;
using System.Text;

using SupportApp.Application.Common.Interfaces;

namespace SupportApp.Infrastructure.Identity
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public bool Verify(string password, string passwordHash)
        {
            var hashOfInput = Hash(password);
            return hashOfInput == passwordHash;
        }
    }
}
