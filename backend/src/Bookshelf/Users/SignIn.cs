using Bookshelf.Users.Exceptions;
using Raven.Client.Documents;

namespace Bookshelf.Users;

public interface ISignIn
{
    Task Execute(Email email, Password password);
}

public class SignIn : ISignIn
{
    private readonly IDocumentStore _documentStore;

    public SignIn(IDocumentStore documentStore) => _documentStore = documentStore;
    
    public async Task Execute(Email email, Password password)
    {
        using var session = _documentStore.OpenAsyncSession();

        var userModel = await session.LoadAsync<UserDocument>(email.ToString());
        if (userModel is not null)
            throw new EmailAlreadyInUseException(email);

        var user = User.Create(email, password).ToDocument();
        await session.StoreAsync(user);
    }
}

public static class SignInExtensions
{
    public static Task ExecuteWithPrimitive(this ISignIn signIn, string email, string password) =>
        signIn.Execute(
            new Email(email),
            Password.Create(password));
}