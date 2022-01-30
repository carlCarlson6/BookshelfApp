using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleBooks.SDK;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGoogleBooks(this IServiceCollection services, IConfiguration configurationManager)
    {
        var config = configurationManager.GetValue<GoogleBooksSettings>(nameof(GoogleBooksSettings));
        return services.AddSingleton(new GoogleBooks(config));
    }
}