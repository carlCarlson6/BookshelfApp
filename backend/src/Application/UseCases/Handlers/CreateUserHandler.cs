using Application.UseCases.Abstractions;
using Application.UseCases.Handlers.Commands;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IAddNewUser _useCase;

    public CreateUserHandler(IAddNewUser useCase) => _useCase = useCase;

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var (email, password) = await CreateValueObjects(request);

        await _useCase.Execute(email, password);
        
        return Unit.Value;
    }

    // TODO - move to extension
    private static Task<(Email email, Password password)> CreateValueObjects(CreateUserCommand command) =>
        Task.FromResult((
            Email.Create(command.Email), 
            Password.Create(command.Password)));
}