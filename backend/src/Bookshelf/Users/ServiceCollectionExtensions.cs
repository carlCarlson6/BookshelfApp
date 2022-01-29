using Bookshelf.Users.Jwt;

namespace Bookshelf.Users;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfigurationRoot configurationRoot) => 
        services
            .AddSingleton<JwtGenerator>()
            .AddJwtValidation(configurationRoot);

    public static IServiceCollection AddUsersSlice(this IServiceCollection services) =>
        services
            .AddTransient<ISignIn, SignIn>()
            .AddTransient<ILogin, Login>();
}
