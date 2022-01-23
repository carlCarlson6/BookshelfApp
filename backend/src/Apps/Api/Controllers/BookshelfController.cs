using System.Net;
using Api.Controllers.Messages;
using Api.Extensions;
using Application.DTOs;
using Application.UseCases.Handlers.Commands;
using Application.UseCases.Handlers.Queries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("api/bookshelf")]
public class BookshelfController : Controller
{
    private readonly IMediator _mediator;

    public BookshelfController(IMediator mediator) => _mediator = mediator;
    
    [HttpPost(Name = nameof(AddBook))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> AddBook([FromBody] AddBookRequest request)
    {
        var command = new AddBookByIsbnCommand 
        {
            Isbn = request.Isbn,
            Location = request.Location,
            UserId = HttpContext.GetIdentifiedUserId(),
        };

        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (BookNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet(Name = nameof(GetShelf))]
    // TODO - add produces response type
    public async Task<ActionResult<BookshelfDto>> GetShelf([FromQuery] string author, [FromQuery] string location, [FromQuery] string title)
    {
        try
        {
            var query = DetermineQuery(author, location, title);
            return await _mediator.Send(query);;
        }
        // TODO - catch rest of exceptions
        catch (ArgumentOutOfRangeException)
        {
            return BadRequest();
        }
    }

    private IQueryBooksRequest DetermineQuery(string author, string location, string title)
    {
        var userId = HttpContext.GetIdentifiedUserId();

        if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(location) && string.IsNullOrWhiteSpace(title))
        {
            return new QueryAllBooks { UserId = userId };
        }
        if (!string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(location) && string.IsNullOrWhiteSpace(title))
        {
            return new QueryBooksByAuthor { UserId = userId, Author = author};
        }
        if (string.IsNullOrWhiteSpace(author) && !string.IsNullOrWhiteSpace(location) && string.IsNullOrWhiteSpace(title))
        {
            return new QueryBooksByLocation { UserId = userId, Location = location};
        }
        if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(location) && !string.IsNullOrWhiteSpace(title))
        {
            return new QueryBooksByTitle { UserId = userId, Title = title};
        }

        throw new ArgumentOutOfRangeException();
    }
}