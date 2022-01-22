using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services;

public interface IBookFinder
{
    Task<Book?> Search(Isbn isbn);
}