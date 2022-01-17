using Application.DTOs;
using Application.UseCases.Abstractions;
using Application.UseCases.Handlers.Queries;
using Domain.Specifications;
using MediatR;

namespace Application.UseCases.Handlers;

public class QueryBooksHandler : 
    IRequestHandler<QueryAllBooks, BookshelfDto>, 
    IRequestHandler<QueryBooksByLocation, BookshelfDto>,
    IRequestHandler<QueryBooksByAuthor, BookshelfDto>
{
    private readonly IBookshelfRetriever _retriever;

    public QueryBooksHandler(IBookshelfRetriever retriever) => _retriever = retriever;

    public async Task<BookshelfDto> Handle(QueryAllBooks request, CancellationToken cancellationToken)
    {
        var userId = request.ToValueObjects();

        var bookshelf = await _retriever.RetrieveAllUserBooks(userId);

        return bookshelf.ToDto();
    }

    public async Task<BookshelfDto> Handle(QueryBooksByLocation request, CancellationToken cancellationToken)
    {
        var (userId, location) = await request.ToValueObjects();

        var specification = new BooksByLocationSpecification(location);
        var bookshelf = await _retriever.RetrieveAllUserBooksBySpecification(userId, specification);

        return bookshelf.ToDto();
    }

    public async Task<BookshelfDto> Handle(QueryBooksByAuthor request, CancellationToken cancellationToken)
    {
        var (userId, author) = await request.ToValueObjects();

        var specification = new BooksByAuthorSpecification(author);
        var bookshelf = await _retriever.RetrieveAllUserBooksBySpecification(userId, specification);

        return bookshelf.ToDto();
    }
}
