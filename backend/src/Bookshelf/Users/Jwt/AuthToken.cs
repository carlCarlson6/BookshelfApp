using static System.String;

namespace Bookshelf.Users.Jwt;

public record AuthToken
{
    public string Value { get; }
    
    public AuthToken(string value)
    {
        if (IsNullOrEmpty(value) || IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        Value = value;
    }

    public static AuthToken FromAuthorizationBearer(string authorizationHeader) =>
        new(authorizationHeader.Replace("Bearer ", ""));

    public string WithBearer() => $"Bearer {Value}";
}
