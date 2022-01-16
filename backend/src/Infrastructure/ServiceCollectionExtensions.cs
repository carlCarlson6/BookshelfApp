using Domain.Services;
using Infrastructure.GoogleBooks;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGoogleBooksApi(this IServiceCollection services)
    {
        // TODO - add configuration as singleton
        return services.AddSingleton<IBookFinderApi, BookFinderGoogleApi>();
    }
}