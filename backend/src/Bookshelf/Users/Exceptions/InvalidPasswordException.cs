namespace Bookshelf.Users.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base(nameof(InvalidPasswordException)) { }
}
