using System.Security.Cryptography;

namespace Bookshelf.Users;

internal static class Hashing 
{
    public static byte[] GenerateSalt(int saltLength) => RandomNumberGenerator.GetBytes(saltLength);

    public static byte[] GenerateHash(string input, byte[] salt, int hashLength) =>
        new Rfc2898DeriveBytes(input, salt, 100000)
            .GetBytes(hashLength);

    public static string CombineHashAndSaltToString(byte[] hash, byte[] salt)
    {
        var hashedPasswordBytes = new byte[salt.Length + hash.Length];
        Array.Copy(salt, 0, hashedPasswordBytes, 0, salt.Length);
        Array.Copy(hash, 0, hashedPasswordBytes, salt.Length, hash.Length);
        return Convert.ToBase64String(hashedPasswordBytes);
    }
    
    public static bool ValidateHashes(byte[] inputHashA, byte[] inputHashB)
    {
        for (var i=0; i < 20; i++)
            if (inputHashA[i + 16] != inputHashB[i])
                return false;
        return true;
    }
}