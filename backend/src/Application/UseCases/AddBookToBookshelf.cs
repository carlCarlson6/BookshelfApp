using Application.Abstractions;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.UseCases;

public class AddBookToBookshelf : IAddBookToBookshelf
{
    private readonly IBookshelfRepository _bookshelfRepository;

    public AddBookToBookshelf(IBookshelfRepository bookshelfRepository) => _bookshelfRepository = bookshelfRepository;

    public async Task Execute(UserId userId, Book book)
    {
        var bookshelf = await _bookshelfRepository.Read(userId) ?? Bookshelf.CreateEmpty(userId);

        bookshelf.AddBook(book);

        await _bookshelfRepository.Save(bookshelf);
    }
}