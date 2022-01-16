using System.Collections.ObjectModel;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Bookshelf
{
    public BookshelfId Id { get; }
    public UserId Owner { get; }
    
    public ReadOnlyCollection<Book> Books => _books.AsReadOnly();
    private List<Book> _books;

    public void AddBook(Book book) => _books.Add(book);
}