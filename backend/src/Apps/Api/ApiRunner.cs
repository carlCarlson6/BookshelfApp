using Application;
using Infrastructure;

namespace Api;

public static class ApiRunner
{
    public static void RunApi(string[] arguments)
    {
        var builder = WebApplication.CreateBuilder(arguments);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();
        ConfigureApi(app);

        app.Run();
    }
    
    private static IServiceCollection ConfigureServices(IServiceCollection services, ConfigurationManager configurationManager) => 
        services
            .AddJwt(configurationManager)
            .AddUseCases()
            .AddGoogleBooksApi(configurationManager)
            .AddMongoRepositories(configurationManager);

    private static void ConfigureApi(WebApplication webApp)
    {
        if (webApp.Environment.IsDevelopment())
        {
            webApp.ConfigureDevelopmentApp();
        }   

        webApp
            .UseHttpsRedirection()
            .UseAuthorization();
            
        webApp.MapControllers();
    }
    
    private static void ConfigureDevelopmentApp(this IApplicationBuilder appBuilder) =>
        appBuilder
            .UseSwagger()
            .UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
}