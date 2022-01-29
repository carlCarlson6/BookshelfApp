using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Bookshelf.Users.Jwt;

public static class JwtValidation
{
    internal static IServiceCollection AddJwtValidation(this IServiceCollection services, IConfiguration configurationManager)
    {
        var jwtConfiguration = configurationManager.GetValue<JwtConfiguration>(nameof(JwtConfiguration));

        services
            .AddAuthentication(ConfigAuthentication)
            .AddJwtBearer(jwtBearerOpts => ConfigJwtBearer(jwtBearerOpts, jwtConfiguration));

        return services;
    }

    private static TokenValidationParameters GetTokenValidationParameters(JwtConfiguration configuration) => new()
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration.Audience,
        ValidIssuer = configuration.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.Secret)),
    };
    
    private static void ConfigJwtBearer(JwtBearerOptions jwtBearerOpts, JwtConfiguration jwtConfiguration)
    {
        jwtBearerOpts.RequireHttpsMetadata = false;
        jwtBearerOpts.SaveToken = true;
        jwtBearerOpts.TokenValidationParameters = GetTokenValidationParameters(jwtConfiguration);
    }
    
    private static void ConfigAuthentication(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}
