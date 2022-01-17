using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Handlers.Commands;

public class AddBookByIsbnCommand : IRequest
{
    public string UserId { get; set; } = null!;
    public string Isbn { get; set; } = null!;
    public string Location { get; set; } = null!;
}

public static class AddBookByIsbnCommandExtensions 
{
    public static Task<(UserId userId, Isbn isbn)> CreateValueObjects(this AddBookByIsbnCommand command) =>
        Task.FromResult((
            new UserId(command.UserId), 
            new Isbn(command.Isbn)));
}