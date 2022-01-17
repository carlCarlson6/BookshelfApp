using Application.Jwt;
using Application.UseCases.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Domain.Specifications;
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
        var user = await FindUserByEmail(inputEmail);
        if (user is null)
            // TODO - create domain exception
            throw new UserNotFoundException(inputEmail);

        user.ValidatePassword(inputPassword);
        
        return _jwtGenerator.Generate(user.Id);
    }

    private async Task<User?> FindUserByEmail(Email email)
    {
        var specification = new UserByEmailSpecification(email);
        return await _userRepository.Read(specification);
    }
}