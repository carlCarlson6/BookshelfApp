using Domain.Entities;

namespace Infrastructure.MongoRepositories.Models;

public class BookshelfModel
{
    public string Owner { get; set; }
    
    public Bookshelf ToDomain() => throw new NotImplementedException();
    public static BookshelfModel CreateModel(Bookshelf bookshelf) => throw new NotImplementedException();
}