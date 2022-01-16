using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services;

public interface IBookFinderApi
{
    Task<Book?> Search(Isbn isbn);
}