using Bookshelf.Users.Exceptions;
using static System.String;
using static Bookshelf.Users.Hashing;

namespace Bookshelf.Users;

public class User
{
    public UserId Id { get; }
    public Email Email { get; }
    public Password Password { get; }

    public User(UserId id, Email email, Password password) => (Id, Email, Password) = (id, email, password);
    public static User Create(Email email, Password password) => new(UserId.Generate(), email, password);

    public void ValidatePassword(Password inputPassword)
    {
        var isValidPassword = inputPassword.Validate(inputPassword);
        if (!isValidPassword)
        {
            throw new InvalidPasswordException();
        }
    }
}

public record UserId
{
    private readonly string _id;
    
    public UserId(string id) => _id = Guid.Parse(id).ToString();
     
    public override string ToString() => _id;
    
    public static UserId Generate() => new(Guid.NewGuid().ToString());
}

public record Email(string Value)
{
    public override string ToString() => Value;
}

public record Password
{
    private readonly string _hashedPassword;

    public Password(string hashedPassword)
    {
        if (IsNullOrEmpty(hashedPassword) || IsNullOrWhiteSpace(hashedPassword))
            throw new ArgumentNullException(nameof(hashedPassword));

        _hashedPassword = hashedPassword;
    }
    
    public static Password Create(string inputPassword)
    {
        const int saltLength = 16;
        const int hashLength = 20;
        
        var salt = GenerateSalt(saltLength);
        var hash = GenerateHash(inputPassword, salt, hashLength);
        var hashedPassword = CombineHashAndSaltToString(hash, salt);
        
        return new Password(hashedPassword);
    }

    public bool Validate(Password inputPassword)
    {
        var hashBytesA = Convert.FromBase64String(_hashedPassword);
        var hashBytesB = Convert.FromBase64String(inputPassword.ToString());
        return ValidateHashes(hashBytesA, hashBytesB);
    }
    
    public override string ToString() => _hashedPassword;
}
