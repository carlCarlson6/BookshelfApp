using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IAddBookToBookshelf
{
    Task Execute(UserId userId, Book book);
}