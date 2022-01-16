namespace Application.Jwt;

public class JwtConfiguration
{
    public string Secret { get; }
    public string Issuer { get; }
    public string Audience { get; }
}