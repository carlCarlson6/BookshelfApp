namespace Application.UseCases.Handlers.Queries;

public class QueryBooksByTitle : IQueryBooksRequest
{
    public string UserId { get; set; } = null!;
    public string Title { get; set; } = null!;
}