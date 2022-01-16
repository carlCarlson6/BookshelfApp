using Domain.ValueObjects;

namespace Domain.Exceptions;

public class BookNotFoundException : Exception
{
    public BookNotFoundException(Isbn isbn) : base($"{nameof(BookNotFoundException)} - {isbn}") { }
}