using Application.UseCases.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.UseCases;

public class AddNewUser : IAddNewUser
{
    private readonly IUserRepository _userRepository;

    public AddNewUser(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task Execute(Email email, Password password)
    {
        var userFound = await _userRepository.Read(email);
        if (userFound is not null)
            throw new EmailAlreadyInUseException(email);

        var user = User.Create(email, password);
        await _userRepository.Save(user);
    }
}