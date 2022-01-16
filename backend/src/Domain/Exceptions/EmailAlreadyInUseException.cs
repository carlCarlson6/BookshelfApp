using Domain.ValueObjects;

namespace Domain.Exceptions;

public class EmailAlreadyInUseException : Exception
{
    public EmailAlreadyInUseException(Email email) :
        base($"{nameof(EmailAlreadyInUseException)} - {email}") { }
}