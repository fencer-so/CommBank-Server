using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public class GoalsService : IGoalsService
{
    private readonly IMongoCollection<Goal> _goalsCollection;

    public GoalsService(IMongoDatabase mongoDatabase)
    {
        // 🔍 DEBUG: Print DB name
        Console.WriteLine("DEBUG DB: " + mongoDatabase.DatabaseNamespace.DatabaseName);

        // 🔍 DEBUG: Collection name
        Console.WriteLine("DEBUG Collection: Goals");

        _goalsCollection = mongoDatabase.GetCollection<Goal>("Goals");
    }

    public async Task<List<Goal>> GetAsync()
    {
        var goals = await _goalsCollection.Find(_ => true).ToListAsync();

        // 🔍 DEBUG: Count of records
        Console.WriteLine("DEBUG Goals count = " + goals.Count);

        // 🔍 DEBUG: Print each goal name
        foreach (var goal in goals)
        {
            Console.WriteLine("DEBUG Goal: " + goal.Name);
        }

        return goals;
    }

    public async Task<List<Goal>?> GetForUserAsync(string id)
    {
        var goals = await _goalsCollection.Find(x => x.UserId == id).ToListAsync();
        Console.WriteLine("DEBUG GetForUser count = " + goals.Count);
        return goals;
    }

    public async Task<Goal?> GetAsync(string id)
    {
        var goal = await _goalsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        Console.WriteLine("DEBUG Get by ID = " + (goal != null ? goal.Name : "NULL"));
        return goal;
    }

    public async Task CreateAsync(Goal newGoal)
    {
        Console.WriteLine("DEBUG Creating goal: " + newGoal.Name);
        await _goalsCollection.InsertOneAsync(newGoal);
    }

    public async Task UpdateAsync(string id, Goal updatedGoal)
    {
        Console.WriteLine("DEBUG Updating goal: " + id);
        await _goalsCollection.ReplaceOneAsync(x => x.Id == id, updatedGoal);
    }

    public async Task RemoveAsync(string id)
    {
        Console.WriteLine("DEBUG Deleting goal: " + id);
        await _goalsCollection.DeleteOneAsync(x => x.Id == id);
    }
}