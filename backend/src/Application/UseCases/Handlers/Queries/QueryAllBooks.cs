namespace Application.UseCases.Handlers.Queries;

public class QueryAllBooks : IQueryBooksRequest
{
    public string UserId { get; set; } = null!;
}