using MediatR;

namespace Application.UseCases.Handlers.Commands;

public class AddBookByIsbnCommand : IRequest
{
    public string UserId { get; set; } = null!;
    public string Isbn { get; set; } = null!;
    public string Location { get; set; } = null!;
}
