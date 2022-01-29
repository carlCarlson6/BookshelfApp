using Bookshelf.Users.Exceptions;
using Raven.Client.Documents;

namespace Bookshelf.Users;

public static class UserExtensions
{
    public static UserDocument ToModel(this User user) => new()
    {
        Id = UserDocument.DocumentId(user),
        Email = user.Email.ToString(),
        Password = user.Password.ToString()
    };

    public static User ToDomain(this UserDocument userDocument) => new(
        new UserId(userDocument.Id),
        new Email(userDocument.Email),
        new Password(userDocument.Password));
    
    public static async Task<User> SingleOrDefaultUserByEmail(this IDocumentStore documentStore, Email email)
    {
        using var session = documentStore.OpenAsyncSession();

        var userDocument = await session.Query<UserDocument>()
            .Where(u => u.Email == email.ToString())
            .SingleOrDefaultAsync();
        
        if (userDocument is null) 
            throw new UserNotFoundException(email);

        return userDocument.ToDomain();
    }
}
