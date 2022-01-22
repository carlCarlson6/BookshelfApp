using Domain.Services;
using Infrastructure.GoogleBooks;
using Infrastructure.MongoRepositories;
using Infrastructure.MongoRepositories.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGoogleBooksApi(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var config = configurationManager.GetValue<GoogleBooksApiConfiguration>(nameof(GoogleBooksApiConfiguration));

        return services
            .AddSingleton(config)
            .AddSingleton<IBookFinder, BookFinderGoogle>();
    }
    
    public static IServiceCollection AddMongoRepositories(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var config = configurationManager.GetValue<MongoDbConfiguration>(nameof(MongoDbConfiguration));

        var client = new MongoClient(config.ConnectionString);
        var database = client.GetDatabase(config.DatabaseName);

        return services
            .AddUserRepository(database, config)
            .AddBookshelfRepository(database, config);
    }

    private static IServiceCollection AddUserRepository(this IServiceCollection services, IMongoDatabase database, MongoDbConfiguration mongoDbConfiguration) =>
        services
            .AddSingleton(database.GetCollection<UserModel>(mongoDbConfiguration.UserCollectionName))
            .AddSingleton<IUserRepository, UserMongoRepository>();
    
    private static IServiceCollection AddBookshelfRepository(this IServiceCollection services, IMongoDatabase database, MongoDbConfiguration mongoDbConfiguration) =>
        services
            .AddSingleton(database.GetCollection<UserModel>(mongoDbConfiguration.BookshelfCollectionName))
            .AddSingleton<IBookshelfRepository, BookshelfMongoRepository>();
}