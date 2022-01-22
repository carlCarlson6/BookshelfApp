using Application.Jwt;
using Application.UseCases.Abstractions;
using Domain.Exceptions;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.UseCases;

public class AuthenticateUser : IAuthenticateUser
{
    private readonly JwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticateUser(JwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthToken> Execute(Email inputEmail, Password inputPassword)
    {
        var user = await _userRepository.Read(inputEmail);
        if (user is null)
            throw new UserNotFoundException(inputEmail);

        user.ValidatePassword(inputPassword);
        
        return _jwtGenerator.Generate(user.Id);
    }
}