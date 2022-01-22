using Application.UseCases.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.UseCases;

public class AddBookToBookshelf : IAddBookToBookshelf
{
    private readonly IBookshelfRepository _bookshelfRepository;
    private readonly IBookFinder _bookFinder;

    public AddBookToBookshelf(IBookshelfRepository bookshelfRepository, IBookFinder bookFinder)
    {
        _bookshelfRepository = bookshelfRepository;
        _bookFinder = bookFinder;
    }

    public async Task Execute(UserId userId, Isbn isbn, string location)
    {
        if (userId is null) throw new ArgumentNullException(nameof(userId));
        if (isbn is null) throw new ArgumentNullException(nameof(isbn));
        if (string.IsNullOrWhiteSpace(location)) throw new ArgumentNullException(nameof(location));

        var bookshelf = await _bookshelfRepository.Read(userId) ?? Bookshelf.CreateEmpty(userId);

        var book = await _bookFinder.Search(isbn);
        if (book is null)
            throw new BookNotFoundException(isbn);

        book.AddLocation(location);
        
        bookshelf.AddBookToShelf(book);

        await _bookshelfRepository.Save(bookshelf);
    }
}