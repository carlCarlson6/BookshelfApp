namespace Domain.ValueObjects;

public class Book
{
    public string Isbn { get; }
    public string Location { get; }
    public string Title { get; }
    public string Author { get; }
    
    public Book(string isbn, string location, string title, string author)
    {
        Isbn = isbn;
        Location = location;
        Title = title;
        Author = author;
    }
}