using Domain.ValueObjects;

namespace Domain.Entities;

public class Book
{
    public Isbn Isbn { get; }
    public string Title { get; }
    public string Author { get; }
    public string Location { get; private set; }
    public string Publisher { get; }
    public DateTime PublishedDate { get; }
    public string Description { get; }
    public uint PageCount { get; }
    public string ThumbnailImagePath { get; }
    public string SmallThumbnailImagePath { get; }
    
    public Book(Isbn isbn, string title, string author, string location, string publisher, DateTime publishedDate, string description, uint pageCount, string thumbnailImagePath, string smallThumbnailImagePath)
    {
        Isbn = isbn;
        Title = title;
        Author = author;
        Location = location;
        Publisher = publisher;
        PublishedDate = publishedDate;
        Description = description;
        PageCount = pageCount;
        ThumbnailImagePath = thumbnailImagePath;
        SmallThumbnailImagePath = smallThumbnailImagePath;
    }
    
    public void AddLocation(string location) => Location = location;

    public bool Equals(Book book) => Isbn.Equals(book.Isbn);
}