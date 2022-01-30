namespace Bookshelf.Bookshelfs.Exceptions;

public class BookNotFoundException : Exception
{
    public BookNotFoundException(Isbn isbn) : base($"{nameof(BookNotFoundException)} - {isbn}") { }
}