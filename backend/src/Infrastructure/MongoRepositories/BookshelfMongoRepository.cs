using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;

namespace Infrastructure.MongoRepositories;

public class BookshelfMongoRepository : IBookshelfRepository
{
    public Task Save(Bookshelf bookshelf) => throw new NotImplementedException();

    public Task<Bookshelf?> Read(UserId id) => throw new NotImplementedException();
}