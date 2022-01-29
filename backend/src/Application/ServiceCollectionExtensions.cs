using Application.Jwt;
using Application.UseCases;
using Application.UseCases.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services, ConfigurationManager configurationManager) => 
        services
            .AddSingleton<JwtGenerator>()
            .AddJwtValidation(configurationManager);
    
    
    public static IServiceCollection AddUseCases(this IServiceCollection services) => 
        services
            .AddTransient<IAddBookToBookshelf, AddBookToBookshelf>()
            .AddTransient<IAuthenticateUser, AuthenticateUser>()
            .AddTransient<IBookshelfRetriever, BookshelfRetriever>();
}