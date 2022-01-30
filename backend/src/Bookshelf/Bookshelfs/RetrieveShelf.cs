using Bookshelf.Bookshelfs.Specifications;
using Bookshelf.Users;
using Raven.Client.Documents;

namespace Bookshelf.Bookshelfs;

public interface IRetrieveShelf
{
    Task<Bookshelf> Execute(UserId userId);
    Task<Bookshelf> ExecuteBySpecification(UserId userId, Specification<Book> specification);
}

public class RetrieveShelf : IRetrieveShelf
{
    private readonly IDocumentStore _documentStore;

    public RetrieveShelf(IDocumentStore documentStore) => _documentStore = documentStore;

    public Task<Bookshelf> Execute(UserId userId) => Find(userId);

    public async Task<Bookshelf> ExecuteBySpecification(UserId userId, Specification<Book> specification)
    {
        var bookshelf = await Find(userId);
        var filteredBook = bookshelf.FilterBySpecification(specification);
        return new Bookshelf(bookshelf.Id, bookshelf.Owner, filteredBook);
    }

    private async Task<Bookshelf> Find(UserId userId)
    {
        // TODO - add index
        var bookshelfDocument = await _documentStore.OpenAsyncSession()
            .Query<BookshelfDocument>()
            .Where(b => b.Owner == userId.ToString())
            .FirstOrDefaultAsync();

        return bookshelfDocument is null ? 
            Bookshelf.CreateEmpty(userId) : 
            bookshelfDocument.ToDomain();
    }
}

public static class RetrieveShelfExtensions
{
    public static Task<Bookshelf> ExecuteWithPrimitives(this IRetrieveShelf retrieveShelf, string userId) =>
        retrieveShelf.Execute(
            new UserId(userId));

    public static Task<Bookshelf> ExecuteBySpecificationWithPrimitives(this IRetrieveShelf retrieveShelf, string userId, Specification<Book> specification) =>
        retrieveShelf.ExecuteBySpecification(
            new UserId(userId),
            specification);
}