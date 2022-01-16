using Domain.Entities;
using Domain.Services;
using Domain.Specifications;
using Domain.ValueObjects;
using Infrastructure.MongoRepositories.Models;
using MongoDB.Driver;

namespace Infrastructure.MongoRepositories;

public class UserMongoRepository : IUserRepository
{
    private readonly IMongoCollection<UserModel> _userModels;

    public UserMongoRepository(IMongoCollection<UserModel> userModels) => _userModels = userModels;

    public Task Save(User user) => throw new NotImplementedException();

    public Task<IEnumerable<User>> Read() => throw new NotImplementedException();

    public async Task<User?> Read(UserId id)
    {
        var userModel = await _userModels.Find(u => u.Id.Equals(id.ToString())).FirstOrDefaultAsync();
        return userModel?.ToDomain();
    }

    public Task<User?> Read(Specification<Email> specification) => throw new NotImplementedException();

    public Task Update(User user) => throw new NotImplementedException();

    public Task Delete(UserId id) => throw new NotImplementedException();
}
