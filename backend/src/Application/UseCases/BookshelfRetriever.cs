using Application.UseCases.Abstractions;
using Domain.Entities;
using Domain.Services;
using Domain.Specifications;
using Domain.ValueObjects;

namespace Application.UseCases;

public class BookshelfRetriever : IBookshelfRetriever
{
    private readonly IBookshelfRepository _bookshelfRepository;

    public BookshelfRetriever(IBookshelfRepository bookshelfRepository) => _bookshelfRepository = bookshelfRepository;

    public Task<Bookshelf> RetrieveAllUserBooks(UserId userId) => FindBookshelf(userId);

    public async Task<Bookshelf> RetrieveAllUserBooksBySpecification(UserId userId, Specification<Book> specification)
    {
        var bookshelf = await FindBookshelf(userId);
        var filteredBooks = bookshelf.FilterBySpecification(specification);
        return new Bookshelf(bookshelf.Id, bookshelf.Owner, filteredBooks);
    }
    
    private async Task<Bookshelf> FindBookshelf(UserId userId) => 
        await _bookshelfRepository.Read(userId) ?? Bookshelf.CreateEmpty(userId);
}