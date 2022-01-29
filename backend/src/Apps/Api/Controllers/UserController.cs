using System.Net;
using Application.UseCases.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) => _mediator = mediator;

    [HttpPost(Name = nameof(SignIn))]
    [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> SignIn([FromBody] CreateUserCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}