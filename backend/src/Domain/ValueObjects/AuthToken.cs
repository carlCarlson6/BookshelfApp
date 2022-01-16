namespace Domain.ValueObjects;

public class AuthToken : StringValueObject
{
    public AuthToken(string inputIsbn) : base(inputIsbn) { }

    public static AuthToken FromAuthorizationBearer(string authorizationHeader) =>
        new(authorizationHeader.Replace("Bearer ", ""));
}