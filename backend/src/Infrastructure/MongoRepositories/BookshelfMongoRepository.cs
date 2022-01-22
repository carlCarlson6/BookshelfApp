using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using Infrastructure.MongoRepositories.Models;
using MongoDB.Driver;

namespace Infrastructure.MongoRepositories;

public class BookshelfMongoRepository : IBookshelfRepository
{
    private readonly IMongoCollection<BookshelfModel> _bookshelfModels;

    public BookshelfMongoRepository(IMongoCollection<BookshelfModel> bookshelfModels) => 
        _bookshelfModels = bookshelfModels;

    public async Task Save(Bookshelf bookshelf)
    {
        var bookshelfModel = BookshelfModel.CreateModel(bookshelf);

        var result = await _bookshelfModels.ReplaceOneAsync(
            b => b.Owner == bookshelfModel.Owner,
            bookshelfModel,
            new UpdateOptions
            {
                IsUpsert = true
            });

        if (!result.IsAcknowledged || result.ModifiedCount != 0)
            // TODO - replace by proper exception
            throw new Exception();
    }

    public async Task<Bookshelf?> Read(UserId id)
    {
        var bookshelfModel = await _bookshelfModels
            .Find(b => b.Owner == id.ToString())
            .FirstOrDefaultAsync();

        return bookshelfModel?.ToDomain();
    }
}