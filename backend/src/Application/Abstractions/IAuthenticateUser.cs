using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IAuthenticateUser
{
    public Task<AuthToken> Execute(Email inputEmail, Password inputPassword);
}