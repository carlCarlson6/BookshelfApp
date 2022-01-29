namespace Bookshelf.Users.Exceptions;

public class EmailAlreadyInUseException : Exception
{
    public EmailAlreadyInUseException(Email email) : base($"{nameof(EmailAlreadyInUseException)} - {email}") { }
}
