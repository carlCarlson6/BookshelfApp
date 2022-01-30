using System.Net;
using System.Security.Claims;
using Bookshelf.Bookshelfs.Exceptions;
using Bookshelf.Bookshelfs.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Bookshelfs;

[ApiController]
[Authorize]
[Route("api/bookshelf")]
public class BookshelfController : Controller
{
    private readonly IAddBook _addBook;
    private readonly IRetrieveShelf _retrieve;

    public BookshelfController(IAddBook addBook, IRetrieveShelf retrieve) =>
        (_addBook, _retrieve) = (addBook, retrieve);

    [HttpPost(Name = nameof(AddBook))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> AddBook([FromBody] AddBookRequest request)
    {
        try
        {
            await _addBook.ExecuteWithPrimitives(
                HttpContext.GetIdentifiedUserId(),
                request.Isbn,
                request.Location);
            return Ok();
        }
        catch (BookNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet(Name = nameof(GetShelf))]
    // TODO - add produces response type
    public async Task<ActionResult<Bookshelf>> GetShelf([FromQuery] string author, [FromQuery] string location, [FromQuery] string title)
    {
        try
        {
            var userId = HttpContext.GetIdentifiedUserId();
            var specification = DetermineSpecification(author, location, title);
            return specification is null ? 
                await _retrieve.ExecuteWithPrimitives(userId) : 
                await _retrieve.ExecuteBySpecificationWithPrimitives(userId, specification);
        }
        catch (ArgumentOutOfRangeException)
        {
            return BadRequest();
        }
    }

    private Specification<Book>? DetermineSpecification(string author, string location, string title)
    {
        if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(location) && string.IsNullOrWhiteSpace(title))
        {
            return null;
        }
        if (!string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(location) && string.IsNullOrWhiteSpace(title))
        {
            return new BooksByAuthorSpecification(author);
        }
        if (string.IsNullOrWhiteSpace(author) && !string.IsNullOrWhiteSpace(location) && string.IsNullOrWhiteSpace(title))
        {
            return new BooksByLocationSpecification(location);
        }
        if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(location) && !string.IsNullOrWhiteSpace(title))
        {
            return new BooksByTitleSpecification(title);
        }

        throw new ArgumentOutOfRangeException();
    }   
}

public class AddBookRequest
{
    public string Isbn { get; set; }
    public string Location { get; set; }
}

public static class HttpContextExtensions
{
    public static string GetIdentifiedUserId(this HttpContext httpContext) => 
        httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
} 
