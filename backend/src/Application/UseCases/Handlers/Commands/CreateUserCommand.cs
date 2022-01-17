using MediatR;

namespace Application.UseCases.Handlers.Commands;

public class CreateUserCommand : IRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}