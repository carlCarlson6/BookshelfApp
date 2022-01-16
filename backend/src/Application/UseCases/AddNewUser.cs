using Application.Abstractions;
using Domain;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;

namespace Application;

public class AddNewUser : IAddNewUser
{
    private readonly IUserRepository _userRepository;

    public AddNewUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Execute(Email email, Password password)
    {
        var userFound = await FindUserByEmail(email);
        if (userFound is not null)
            // TODO - throw domain exception
            throw new Exception();

        var user = User.Create(email, password);
        await _userRepository.Save(user);

        return user;
    }
    
    private async Task<User?> FindUserByEmail(Email email)
    {
        var specification = new UserByEmailSpecification(email);
        return await _userRepository.Read(specification);
    }
}