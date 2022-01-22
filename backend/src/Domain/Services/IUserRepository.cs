using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services;

public interface IUserRepository
{
    Task Save(User user);
    
    Task<IEnumerable<User>> Read();
    Task<User?> Read(UserId id);
    Task<User?> Read(Email email);
    
    Task Update(User user);
    
    Task Delete(UserId id);
}