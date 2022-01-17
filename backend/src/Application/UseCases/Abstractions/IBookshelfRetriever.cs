using Domain.Entities;
using Domain.Specifications;
using Domain.ValueObjects;

namespace Application.UseCases.Abstractions;

public interface IBookshelfRetriever
{
    Task<Bookshelf> RetrieveAllUserBooks(UserId userId);
    Task<Bookshelf> RetrieveAllUserBooksBySpecification(UserId userId, Specification<Book> specification);
}