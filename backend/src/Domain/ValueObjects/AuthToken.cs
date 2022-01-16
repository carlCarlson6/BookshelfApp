namespace Domain.ValueObjects;

public class AuthToken : StringValueObject
{
    public AuthToken(string value) : base(value) { }

    public static AuthToken FromAuthorizationBearer(string authorizationHeader) =>
        new(authorizationHeader.Replace("Bearer ", ""));
}