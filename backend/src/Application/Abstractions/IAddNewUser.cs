using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IAddNewUser
{
    Task<User> Execute(Email email, Password password);
}