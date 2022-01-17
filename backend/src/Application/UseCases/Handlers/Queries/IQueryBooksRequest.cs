using Application.DTOs;
using MediatR;

namespace Application.UseCases.Handlers.Queries;

public interface IQueryBooksRequest : IRequest<BookshelfDto>
{
    string UserId { get; set; }
}