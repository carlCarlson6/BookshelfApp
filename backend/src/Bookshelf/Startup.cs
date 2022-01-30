using Bookshelf.Infrastructure;
using Bookshelf.Users;
using GoogleBooks.SDK;

namespace Bookshelf;

public static class Startup
{
    public static void RunWebApp(string[] arguments) => WebApplication
        .CreateBuilder(arguments)
        .ConfigureServices()
        .Build()
        .ConfigureApplication()
        .Run();

    private static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services
            .AddRavenDatabase(config)
            .AddGoogleBooks(config)
            .AddJwt(config)
            .AddUsersSlice();
        
        return builder;
    }

    private static WebApplication ConfigureApplication(this WebApplication webApp)
    {
        if (webApp.Environment.IsDevelopment()) 
            ConfigureDevelopmentApp(webApp);

        webApp
            .UseHttpsRedirection()
            .UseAuthorization();
            
        webApp.MapControllers();

        return webApp;
    }
    
    private static void ConfigureDevelopmentApp(IApplicationBuilder appBuilder) =>
        appBuilder
            .UseSwagger()
            .UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
}
