using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IAddBookToBookshelf
{
    Task Execute(UserId userId, Isbn book, string location);
}