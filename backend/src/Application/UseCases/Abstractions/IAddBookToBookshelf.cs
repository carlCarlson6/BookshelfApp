using Domain.ValueObjects;

namespace Application.UseCases.Abstractions;

public interface IAddBookToBookshelf
{
    Task Execute(UserId userId, Isbn book, string location);
}