namespace Infrastructure.MongoRepositories;

public class MongoDbConfiguration
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string UserCollectionName { get; set; } = null!;
    public string BookshelfCollectionName { get; set; } = null!;
}