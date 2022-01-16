using Application.Jwt;
using Application.UseCases;
using Application.UseCases.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        // TODO - add configuration as singleton
        return services.AddSingleton<JwtGenerator>();
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services) =>
        services
            .AddTransient<IAddBookToBookshelf, AddBookToBookshelf>()
            .AddTransient<IAddNewUser, AddNewUser>()
            .AddTransient<IAuthenticateUser, AuthenticateUser>()
            .AddTransient<IBookshelfRetriever, BookshelfRetriever>();
}