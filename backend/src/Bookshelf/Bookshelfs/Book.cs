namespace Bookshelf.Bookshelfs;

public record Book(
    Isbn Isbn, 
    string Title,
    string Author,
    string Location,
    string Publisher,
    DateTime PublishedDate,
    string Description,
    string PageCount,
    string ThumbnailImagePath,
    string SmallThumbnailImagePath)
{
    public Book AddLocation(string location) => 
        this with { Location = location };
}