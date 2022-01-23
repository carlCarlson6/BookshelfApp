using System.Net;
using Api.Controllers.Messages;
using Application.UseCases.Handlers.Commands;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost(Name = nameof(LogIn))]
    [ProducesResponseType(typeof(LoginUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<LoginUserResponse>> LogIn([FromBody] AuthenticateUserCommand authenticateUserCommand)
    {
        try
        {
            var token = await _mediator.Send(authenticateUserCommand);
            return new LoginUserResponse { JwtToken = token };
        }
        catch (UserNotFoundException)
        {
            return BadRequest();
        }
        catch (InvalidPasswordException)
        {
            return BadRequest();
        }
    }
}