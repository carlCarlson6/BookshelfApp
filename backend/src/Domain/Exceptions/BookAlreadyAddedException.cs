using Domain.Entities;

namespace Domain.Exceptions;

public class BookAlreadyAddedException : Exception
{
    public BookAlreadyAddedException(Book book) : 
        base($"{nameof(BookAlreadyAddedException)} - {book.Isbn} - {book.Title} {book.Author}") { }
}