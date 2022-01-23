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
    public async Task<ActionResult<BookshelfDto>> GetShelf()
    {
        var query = new QueryAllBooks { UserId = HttpContext.GetIdentifiedUserId() };

        var bookshelf = await _mediator.Send(query);
        return bookshelf;
    }
}