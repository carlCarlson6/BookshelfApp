using MediatR;

namespace Application.UseCases.Handlers.Commands;

public abstract class AuthenticateUserCommand : IRequest<string>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}