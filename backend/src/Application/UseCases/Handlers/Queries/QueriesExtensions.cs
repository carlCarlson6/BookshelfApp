using Domain.ValueObjects;

namespace Application.UseCases.Handlers.Queries;

public static class QueriesExtensions
{
    public static UserId ToValueObjects(this QueryAllBooks query) => new (query.UserId);

    public static Task<(UserId userId, string author)> ToValueObjects(this QueryBooksByAuthor query) =>
        Task.FromResult((
            new UserId(query.UserId), 
            query.Author));
    
    public static Task<(UserId userId, string location)> ToValueObjects(this QueryBooksByLocation query) =>
        Task.FromResult((
            new UserId(query.UserId), 
            query.Location));
    
    public static Task<(UserId userId, string title)> ToValueObjects(this QueryBooksByTitle query) =>
        Task.FromResult((
            new UserId(query.UserId), 
            query.Title));
}
