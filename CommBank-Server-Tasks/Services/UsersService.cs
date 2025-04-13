using Microsoft.Extensions.Options;
using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public class UsersService : IUsersService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UsersService(IMongoDatabase mongoDatabase)
    {
        _usersCollection = mongoDatabase.GetCollection<User>("Users");
    }

    public async Task<List<User>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser)
    {
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

        await _usersCollection.InsertOneAsync(newUser);
    }

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);
}