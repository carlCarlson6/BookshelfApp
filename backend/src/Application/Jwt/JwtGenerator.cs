using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace Application.Jwt;

public class JwtGenerator
{
    private readonly JwtConfiguration _config;

    public JwtGenerator(JwtConfiguration configuration) => _config = configuration;
    
    public AuthToken Generate(UserId id)
    {
        var tokenDescriptor = GenerateTokenDescriptor(id, _config.Secret, _config.Issuer, _config.Audience);
        var token = WriteToken(tokenDescriptor);
        
        return new AuthToken(token);
    }
    
    private static SecurityTokenDescriptor GenerateTokenDescriptor(UserId id, string secret, string issuer, string audience) =>
        new ()
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)), 
                SecurityAlgorithms.Sha512)
        };

    private static string WriteToken(SecurityTokenDescriptor tokenDescriptor)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
