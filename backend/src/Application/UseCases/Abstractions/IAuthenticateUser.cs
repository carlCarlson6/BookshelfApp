using Domain.ValueObjects;

namespace Application.UseCases.Abstractions;

public interface IAuthenticateUser
{
    public Task<AuthToken> Execute(Email inputEmail, Password inputPassword);
}