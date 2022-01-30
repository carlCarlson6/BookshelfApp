using Bookshelf.Users.Exceptions;
using Raven.Client.Documents;

namespace Bookshelf.Users;

public static class UserExtensions
{
    public static UserDocument ToDocument(this User user) => new()
    {
        Id = UserDocument.DocumentId(user),
        Email = user.Email.ToString(),
        Password = user.Password.ToString()
    };

    public static User ToDomain(this UserDocument userDocument) => new(
        new UserId(userDocument.Id),
        new Email(userDocument.Email),
        new Password(userDocument.Password));
}
