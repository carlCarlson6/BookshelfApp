using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.MongoRepositories.Models;

public class UserModel
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;

    public User ToDomain() => new User(new UserId(Id), new Email(Email), new Password(HashedPassword));
}