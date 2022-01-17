using Domain.ValueObjects;

namespace Application.UseCases.Handlers.Commands;

public static class CommandExtensions
{
    public static Task<(UserId userId, Isbn isbn)> ToValueObjects(this AddBookByIsbnCommand command) =>
        Task.FromResult((
            new UserId(command.UserId), 
            new Isbn(command.Isbn)));
    
    public static Task<(Email email, Password password)> ToValueObjects(this AuthenticateUserCommand command) =>
        Task.FromResult((
            Email.Create(command.Email), 
            Password.Create(command.Password)));
    
    public static Task<(Email email, Password password)> ToValueObjects(this CreateUserCommand command) =>
        Task.FromResult((
            Email.Create(command.Email), 
            Password.Create(command.Password)));
}
