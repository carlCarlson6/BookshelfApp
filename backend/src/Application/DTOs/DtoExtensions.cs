using Domain.Entities;

namespace Application.DTOs;

public static class DtoExtensions
{
    public static BookshelfDto ToDto(this Bookshelf bookshelf) => new()
    {
        Id = bookshelf.Id.ToString(),
        Owner = bookshelf.Owner.ToString(),
        Books = bookshelf.Books.ToDto(),
        Locations = bookshelf.Locations
    };
    
    public static IEnumerable<BookDto> ToDto(this IEnumerable<Book> books) => books.Select(ToDto);

    public static BookDto ToDto(this Book book) => throw new NotImplementedException();
}
