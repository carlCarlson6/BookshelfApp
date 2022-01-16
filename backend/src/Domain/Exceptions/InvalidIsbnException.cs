namespace Domain.Exceptions;

public class InvalidIsbnException : Exception
{
    public InvalidIsbnException(string isbn) : 
        base($"{nameof(InvalidIsbnException)} - {isbn}") { }
}