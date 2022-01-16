using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services;

public interface IBookshelfRepository
{
    Task Save(Bookshelf bookshelf);

    Task<Bookshelf?> Read(UserId id);
}