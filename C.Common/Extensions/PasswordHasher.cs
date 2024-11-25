using System.Security.Cryptography;
using System.Text;

namespace C.Common.Extensions;

public static class PasswordHasher
{
    public static string ComputeStringToSha256Hash(string plainText)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

        StringBuilder stringbuilder = new();
        for (int i = 0; i < bytes.Length; i++)
        {
            stringbuilder.Append(bytes[i].ToString("x2"));
        }
        return stringbuilder.ToString();
    }

    public static bool VerifyPasswordHash(string password, string PasswordHash)
    {
        password = ComputeStringToSha256Hash(password);
        if (password == PasswordHash)
        {
            return true;
        }
        return false;
    }
}
