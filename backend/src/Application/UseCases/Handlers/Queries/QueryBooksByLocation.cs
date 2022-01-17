namespace Application.UseCases.Handlers.Queries;

public class QueryBooksByLocation : IQueryBooksRequest
{
    public string UserId { get; set; } = null!;
    public string Location { get; set; } = null!;
}