namespace Bookshelf.Users.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Email email) : base($"{nameof(UserNotFoundException)} - {email}") { }
}
