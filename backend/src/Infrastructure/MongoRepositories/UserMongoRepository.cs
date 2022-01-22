using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using Infrastructure.MongoRepositories.Models;
using MongoDB.Driver;

namespace Infrastructure.MongoRepositories;

public class UserMongoRepository : IUserRepository
{
    private readonly IMongoCollection<UserModel> _userModels;

    public UserMongoRepository(IMongoCollection<UserModel> userModels) => _userModels = userModels;

    public async Task Save(User user)
    {
        var userModel = UserModel.CreateModel(user);
        await _userModels.InsertOneAsync(userModel);
    }

    public async Task<IEnumerable<User>> Read()
    {
        var userModels = await _userModels.Find(u => true).ToListAsync();
        return userModels is null ? new List<User>() : userModels.Select(u => u.ToDomain());
    }

    public async Task<User?> Read(UserId id)
    {
        var userModel = await _userModels
            .Find(u => u.Id.Equals(id.ToString()))
            .FirstOrDefaultAsync();
        return userModel?.ToDomain();
    }

    public async Task<User?> Read(Email email)
    {
        var userModel = await _userModels
            .Find(u => u.Email == email.ToString())
            .FirstOrDefaultAsync();
        return userModel?.ToDomain();
    }

    public async Task Update(User user)
    {
        var userModel = UserModel.CreateModel(user);
        var updateResult = await _userModels.ReplaceOneAsync(u => u.Id == userModel.Id, userModel);

        if (!updateResult.IsAcknowledged || updateResult.ModifiedCount != 0)
            // TODO - replace by proper exception
            throw new Exception();
    }

    public async Task Delete(UserId id)
    {
        var deleteResult = await _userModels.DeleteManyAsync(u => u.Id == id.ToString());
        
        if (!deleteResult.IsAcknowledged || deleteResult.DeletedCount != 0)
            // TODO - replace by proper exception
            throw new Exception();
    }
}
