using Bookshelf.Users.Jwt;
using Raven.Client.Documents;

namespace Bookshelf.Users;

public interface ILogin
{
    Task<AuthToken> Execute(Email userEmail, Password inputPassword);
}

public class Login : ILogin
{
    private readonly IDocumentStore _documentStore;
    private readonly JwtGenerator _jwtGenerator;

    public Login(IDocumentStore documentStore, JwtGenerator jwtGenerator) =>
        (_documentStore, _jwtGenerator) = (documentStore, jwtGenerator);
    
    public async Task<AuthToken> Execute(Email userEmail, Password inputPassword)
    {
        var user = await _documentStore.SingleOrDefaultUserByEmail(userEmail);
        
        user.ValidatePassword(inputPassword);

        return _jwtGenerator.Generate(user.Id);
    }
}

public static class LoginExtensions
{
    public static Task<AuthToken> ExecuteWithPrimitives(this ILogin login, string email, string inputPassword) =>
        login.Execute(
            new Email(email),
            Password.Create(inputPassword));
} 
