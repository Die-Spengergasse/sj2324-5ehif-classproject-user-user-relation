using System.Security.Cryptography;
using System.Text;

namespace sj2324_5ehif_cooking_user.Webapi.Services;

public class PasswordUtils : IPasswordUtils
{
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}

public interface IPasswordUtils
{
    string HashPassword(string password);
}