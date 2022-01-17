using Domain.ValueObjects;

namespace Application.UseCases.Abstractions;

public interface IAddNewUser
{
    Task Execute(Email email, Password password);
}