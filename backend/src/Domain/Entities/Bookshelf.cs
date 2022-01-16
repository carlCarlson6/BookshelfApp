using System.Collections.ObjectModel;
using Domain.Specifications;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Bookshelf
{
    public BookshelfId Id { get; }
    public UserId Owner { get; }
    
    public ReadOnlyCollection<Book> Books => _books.AsReadOnly();
    private List<Book> _books;
    
    public ReadOnlyCollection<string> Locations => 
        _books.Select(book => book.Location).Distinct().ToList().AsReadOnly();

    public Bookshelf(BookshelfId bookshelfId, UserId owner, List<Book> books)
    {
        Id = bookshelfId;
        Owner = owner;
        _books = books;
    }

    public static Bookshelf CreateEmpty(UserId owner) => Create(owner, new List<Book>());
    public static Bookshelf Create(UserId owner, List<Book> books) => new (BookshelfId.Generate(), owner, books);

    public void AddBookToShelf(Book book) => _books.Add(book);

    public IEnumerable<Book> FilterBySpecification(Specification<Book> specification) =>
        _books.FindAll(specification.ToPredicate());

}