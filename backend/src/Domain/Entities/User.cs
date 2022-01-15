using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User
{
    public UserId Id { get; }
    public Email Email { get; }
    public Password Password { get; }

    public User(UserId id, Email email, Password password) => 
        (Id, Email, Password) = (id, email, password);

    public void ValidatePassword(string inputPassword)
    {
        var validation = Password.Validate(inputPassword);
        if (!validation)
        {
            throw new InvalidPasswordException();
        }
    }
}