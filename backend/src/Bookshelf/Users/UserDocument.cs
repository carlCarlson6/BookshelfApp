using Bookshelf.Users.Exceptions;
using Raven.Client.Documents;

namespace Bookshelf.Users;

public class UserDocument
{
    public const string CollectionName = "Users";
    
    public string Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public static string DocumentId(User user) => $"{CollectionName}/{user.Id}";
    public static string DocumentId(UserId id) => $"{CollectionName}/{id}";
    public static string DocumentId(string id) => $"{CollectionName}/{id}";
}

public static class UserDocumentExtensions 
{
    public static async Task<User> SingleOrDefaultUserByEmail(this IDocumentStore documentStore, Email email)
    {
        using var session = documentStore.OpenAsyncSession();

        // TODO - create index
        var userDocument = await session.Query<UserDocument>()
            .Where(u => u.Email == email.ToString())
            .SingleOrDefaultAsync();
        
        if (userDocument is null) 
            throw new UserNotFoundException(email);

        return userDocument.ToDomain();
    }
}
