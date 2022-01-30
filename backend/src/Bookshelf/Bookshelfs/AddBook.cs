using Bookshelf.Users;
using GoogleBooks.SDK;
using Raven.Client.Documents;
using static System.String;

namespace Bookshelf.Bookshelfs;

public interface IAddBook
{
    public Task Execute(UserId userId, Isbn isbn, string location);
}

public class AddBook : IAddBook
{
    private readonly IDocumentStore _documentStore;
    private readonly IGoogleBooks _googleBooks;

    public AddBook(IDocumentStore documentStore, IGoogleBooks googleBooks) =>
        (_documentStore, _googleBooks) = (documentStore, googleBooks);

    public async Task Execute(UserId userId, Isbn isbn, string location)
    {
        if (userId is null) throw new ArgumentNullException(nameof(userId));
        if (isbn is null) throw new ArgumentNullException(nameof(isbn));
        if (IsNullOrWhiteSpace(location)) throw new ArgumentNullException(nameof(location));
        
        var book = await _googleBooks.Search(isbn);
        book = book.AddLocation(location);

        using var session = _documentStore.OpenAsyncSession();
        // TODO - add index
        var bookshelfDocument = await session
            .Query<BookshelfDocument>()
            .Where(b => b.Owner == userId.ToString())
            .FirstOrDefaultAsync();

        var bookshelf = bookshelfDocument is null ? 
            Bookshelf.CreateEmpty(userId) : 
            bookshelfDocument.ToDomain();
        
        bookshelf.AddBookToShelf(book);

        await session.StoreAsync(bookshelf.ToDocument());
        await session.SaveChangesAsync();
    }
}

public static class AddBokExtensions
{
    public static Task ExecuteWithPrimitives(this IAddBook addBook, string userId, string isbn, string location) =>
        addBook.Execute(
            new UserId(userId),
            new Isbn(isbn),
            location);
}
