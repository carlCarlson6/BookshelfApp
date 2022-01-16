using Domain.Entities;
using Domain.ValueObjects;

namespace Application.UseCases.Abstractions;

public interface IBookshelfRetriever
{
    Task<IEnumerable<Book>> RetrieveAllUserBooks(UserId userId);
    Task<IEnumerable<Book>> RetrieveAllUserBooksByTitle(UserId userId, string title);
    Task<IEnumerable<Book>> RetrieveAllUserBooksByAuthor(UserId userId, string author);
    Task<IEnumerable<Book>> RetrieveAllUserBooksByLocation(UserId userId, string location);
}