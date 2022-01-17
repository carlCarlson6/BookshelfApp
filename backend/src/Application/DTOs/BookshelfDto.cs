namespace Application.DTOs;

public class BookshelfDto
{
    public string Id { get; set; }
    public string Owner { get; set; } = null!;
    public IEnumerable<BookDto> Books = null!;
    public IEnumerable<string> Locations = null!;
}
