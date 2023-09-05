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

    public async Task<Goal?> GetAsync(string id)
    {
        return await _goalsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Goal newGoal) =>
        await _goalsCollection.InsertOneAsync(newGoal);

    public async Task UpdateAsync(string id, Goal updatedGoal) =>
        await _goalsCollection.ReplaceOneAsync(x => x.Id == id, updatedGoal);

    public async Task RemoveAsync(string id) =>
        await _goalsCollection.DeleteOneAsync(x => x.Id == id);

    public async Task Init()
    {
        await _goalsCollection.InsertManyAsync(new List<Goal>
        {
             new Goal
             {
                  Id="62a3f587102e921da1253d30",
                   Name="House Down Payment",
                    TargetAmount=10000,
                    // TargetDate=DateTime.Now,
                      Balance= 73501.82,
                      // Created=DateTime.Now,
                       UserId="62a29c15f4605c4c9fa7f306",
             }
        });
    }
}