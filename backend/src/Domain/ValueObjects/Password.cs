using System.Security.Cryptography;

namespace Domain.ValueObjects;

public class Password : StringValueObject
{
    public Password(string value) : base(value) { }

    public static Password Create(string inputPassword)
    {
        const int saltLength = 16;
        const int hashLength = 20;
        
        var salt = HashingUtils.GenerateSalt(saltLength);
        var hash = HashingUtils.GenerateHash(inputPassword, salt, hashLength);
        var hashedPassword = HashingUtils.CombineHashAndSaltToString(hash, salt);
        
        return new Password(hashedPassword);
    }

    public bool Validate(string inputPassword) => Validate(Create(inputPassword));
    
    public bool Validate(Password inputPassword)
    {
        var hashBytesA = Convert.FromBase64String(Value);
        var hashBytesB = Convert.FromBase64String(inputPassword.Value);
        return HashingUtils.ValidateHashes(hashBytesA, hashBytesB);
    } 
}


internal static class HashingUtils {
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