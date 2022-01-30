using Bookshelf.Bookshelfs.Exceptions;
using GoogleBooks.SDK;

namespace Bookshelf.Bookshelfs;

public static class GoogleBooksExtensions
{
    public static async Task<Book> Search(this IGoogleBooks googleBooks, Isbn isbn)
    {
        var googleBooksResponse = await googleBooks.Search(isbn.ToString());
        if (googleBooksResponse is null)
            throw new BookNotFoundException(isbn);

        return googleBooksResponse.ToDomain();
    }

    public static Book ToDomain(this GoogleBooksResponse googleBooksResponse) => throw new NotImplementedException();
}