using Application.UseCases.Abstractions;
using Application.UseCases.Handlers.Commands;
using MediatR;

namespace Application.UseCases.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IAddNewUser _useCase;

    public CreateUserHandler(IAddNewUser useCase) => _useCase = useCase;

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var (email, password) = await request.ToValueObjects();

        await _useCase.Execute(email, password);
        
        return Unit.Value;
    }
}
