using Domain.Entities;

namespace Application.DTOs;

public class BookshelfDto
{
    public string Id { get; set; }
    public string Owner { get; set; } = null!;
    public IEnumerable<BookDto> Books = null!;
    public IEnumerable<string> Locations = null!;
}

public static class BookshelfExtensions
{
    public static BookshelfDto ToDto(this Bookshelf bookshelf) => new()
    {
        Id = bookshelf.Id.ToString(),
        Owner = bookshelf.Owner.ToString(),
        Books = bookshelf.Books.ToDto(),
        Locations = bookshelf.Locations
    };
}