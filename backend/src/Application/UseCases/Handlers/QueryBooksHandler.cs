using Application.DTOs;
using Application.UseCases.Abstractions;
using Application.UseCases.Handlers.Queries;
using Domain.ValueObjects;
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
        var userId = new UserId(request.UserId);

        var bookshelf = await _retriever.RetrieveAllUserBooks(userId);

        return bookshelf.ToDto();
    }

    public Task<BookshelfDto> Handle(QueryBooksByLocation request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<BookshelfDto> Handle(QueryBooksByAuthor request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}