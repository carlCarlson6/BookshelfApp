using System.Collections.ObjectModel;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Bookshelf
{
    public BookshelfId Id { get; }
    public UserId Owner { get; }
    
    public ReadOnlyCollection<Book> Books => _books.AsReadOnly();
    private List<Book> _books;

    public Bookshelf(BookshelfId bookshelfId, UserId owner, List<Book> books)
    {
        Id = bookshelfId;
        Owner = owner;
        _books = books;
    }

    public static Bookshelf CreateEmpty(UserId owner) => Create(owner, new List<Book>());
    public static Bookshelf Create(UserId owner, List<Book> books) => new(BookshelfId.Generate(), owner, books);

    public void AddBook(Book book) => _books.Add(book);
}