using Domain.Entities;
using Domain.ValueObjects;

namespace Application.UseCases.Abstractions;

public interface IAddNewUser
{
    Task<User> Execute(Email email, Password password);
}