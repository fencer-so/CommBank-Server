using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public class GoalsService : IGoalsService
{
    private readonly IMongoCollection<Goal> _goalsCollection;

    public GoalsService(IMongoDatabase mongoDatabase)
    {
        _goalsCollection = mongoDatabase.GetCollection<Goal>("Goals");
    }

    public async Task<List<Goal>> GetAsync() =>
        await _goalsCollection.Find(_ => true).ToListAsync();

    public async Task<List<Goal>?> GetForUserAsync(string id) =>
        await _goalsCollection.Find(x => x.UserId == id).ToListAsync();

    public async Task<Goal?> GetAsync(string id) =>
        await _goalsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Goal newGoal) =>
        await _goalsCollection.InsertOneAsync(newGoal);

    // Updated method to support partial updates
    public async Task UpdateAsync(string id, UpdateDefinition<Goal> updateDefinition) =>
        await _goalsCollection.UpdateOneAsync(x => x.Id == id, updateDefinition);

    public async Task RemoveAsync(string id) =>
        await _goalsCollection.DeleteOneAsync(x => x.Id == id);
}
