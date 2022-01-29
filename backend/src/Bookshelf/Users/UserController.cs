using System.Net;
using Bookshelf.Users.Exceptions;
using Bookshelf.Users.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Users;

[ApiController]
[Route("api")]
public class UserController : Controller
{
    private readonly ILogin _login;
    private readonly ISignIn _signIn;

    public UserController(ILogin login, ISignIn signIn)
    {
        _login = login;
        _signIn = signIn;
    }
    
    [Route("auth")]
    [HttpPost(Name = nameof(LogIn))]
    [ProducesResponseType(typeof(AuthToken), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<AuthToken>> LogIn([FromBody] AuthenticateUserCommand authenticateUserCommand)
    {
        try
        {
            return await _login.ExecuteWithPrimitives(authenticateUserCommand.Email, authenticateUserCommand.Password);
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
    
    [Route("users")]
    [HttpPost(Name = nameof(SignIn))]
    [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> SignIn([FromBody] CreateUserCommand command)
    {
        try
        {
            await _signIn.ExecuteWithPrimitive(command.Email, command.Password);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}

public class AuthenticateUserCommand
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateUserCommand
{
    public string Email { get; set; }
    public string Password { get; set; }
}