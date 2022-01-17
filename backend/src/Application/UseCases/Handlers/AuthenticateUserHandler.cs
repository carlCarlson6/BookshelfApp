using Application.UseCases.Abstractions;
using Application.UseCases.Handlers.Commands;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Handlers;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, string>
{
    private readonly IAuthenticateUser _useCase;

    public AuthenticateUserHandler(IAuthenticateUser useCase) => _useCase = useCase;

    public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var (email, password) = await CreateValueObjects(request);
        
        var authToken = await _useCase.Execute(email, password);
        
        return authToken.ToStringWithBearer();
    }

    // TODO - move to extenion
    private static Task<(Email email, Password password)> CreateValueObjects(AuthenticateUserCommand command) =>
        Task.FromResult((
            Email.Create(command.Email), 
            Password.Create(command.Password)));
}