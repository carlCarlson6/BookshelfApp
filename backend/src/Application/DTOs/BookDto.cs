using Domain.Entities;

namespace Application.DTOs;

public class BookDto
{
    public string Isbn { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Publisher { get; set; } = null!;
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; } = null!;
    public uint PageCount { get; set; }
    public string ThumbnailImagePath { get; set; } = null!;
    public string SmallThumbnailImagePath { get; set; } = null!;
}

public static class BookExtensions
{
    public static IEnumerable<BookDto> ToDto(this IEnumerable<Book> books) => books.Select(ToDto);

    public static BookDto ToDto(this Book book) => new BookDto
    {

    };
}