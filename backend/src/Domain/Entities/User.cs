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

    public static User Create(Email email, Password password) => new (UserId.Generate(), email, password);
    
    public void ValidatePassword(Password inputPassword)
    {
    }
}