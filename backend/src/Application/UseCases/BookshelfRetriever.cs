using Application.UseCases.Abstractions;
using Domain;
using Domain.Entities;
using Domain.Services;
using Domain.Specifications;
using Domain.ValueObjects;

namespace Application.UseCases;

public class BookshelfRetriever : IBookshelfRetriever
{
    private readonly IBookshelfRepository _bookshelfRepository;

    public BookshelfRetriever(IBookshelfRepository bookshelfRepository) => _bookshelfRepository = bookshelfRepository;

    public async Task<IEnumerable<Book>> RetrieveAllUserBooks(UserId userId)
    {
        var bookshelf = await FindBookshelf(userId);
        return bookshelf.Books;
    }

    public async Task<IEnumerable<Book>> RetrieveAllUserBooksByTitle(UserId userId, string title)
    {
        var bookshelf = await FindBookshelf(userId);
        var specification = new BooksByTitleSpecification(title);
        return bookshelf.FilterBySpecification(specification);
    }

    public async Task<IEnumerable<Book>> RetrieveAllUserBooksByAuthor(UserId userId, string author)
    {
        var bookshelf = await FindBookshelf(userId);
        var specification = new BooksByAuthorSpecification(author);
        return bookshelf.FilterBySpecification(specification);
    }

    public async Task<IEnumerable<Book>> RetrieveAllUserBooksByLocation(UserId userId, string location)
    {
        var bookshelf = await FindBookshelf(userId);
        var specification = new BooksByLocationSpecification(location);
        return bookshelf.FilterBySpecification(specification);
    }

    private async Task<Bookshelf> FindBookshelf(UserId userId) => 
        await _bookshelfRepository.Read(userId) ?? Bookshelf.CreateEmpty(userId);
}