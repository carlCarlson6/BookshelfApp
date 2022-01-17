using Application.UseCases.Abstractions;
using Application.UseCases.Handlers.Commands;
using MediatR;

namespace Application.UseCases.Handlers;

public class AddBookHandler : IRequestHandler<AddBookByIsbnCommand>
{
    private readonly IAddBookToBookshelf _addBookToBookshelf;

    public AddBookHandler(IAddBookToBookshelf addBookToBookshelf)
    {
        _addBookToBookshelf = addBookToBookshelf;
    }

    public async Task<Unit> Handle(AddBookByIsbnCommand request, CancellationToken cancellationToken)
    {
        var (userId, isbn) = await request.CreateValueObjects();

        await _addBookToBookshelf.Execute(userId, isbn, request.Location);

        return Unit.Value;
    }
}