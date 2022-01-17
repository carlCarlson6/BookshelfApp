namespace Application.UseCases.Handlers.Queries;

public class QueryBooksByAuthor : IQueryBooksRequest
{
    public string UserId { get; set; } = null!;
    public string Author { get; set; } = null!;
}