using CommBank.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CommBank.Services;

public class GoalsService : IGoalsService
{
    private readonly IMongoCollection<Goal> _goalsCollection;
    private readonly IMongoDatabase _database;

    public GoalsService(IMongoDatabase mongoDatabase)
    {
        _database = mongoDatabase;
        _goalsCollection = mongoDatabase.GetCollection<Goal>("goals");
    }

    public async Task<List<Goal>> GetAsync()
    {
       
        Console.WriteLine($"\n[DEBUG] Database Name: {_database.DatabaseNamespace.DatabaseName}");
        Console.WriteLine($"[DEBUG] Collection Name: {_goalsCollection.CollectionNamespace.CollectionName}");

    
        var goals = await _goalsCollection.Find(_ => true).ToListAsync();
        Console.WriteLine($"[DEBUG] Goal Model 撈出的筆數: {goals.Count}");

        
        var rawCollection = _database.GetCollection<BsonDocument>("goals");
        var rawDocs = await rawCollection.Find(new BsonDocument()).ToListAsync();
        Console.WriteLine($"[DEBUG] BsonDocument 撈出的筆數: {rawDocs.Count}\n");

        return goals;
    }

    public async Task<List<Goal>?> GetForUserAsync(string id) =>
        await _goalsCollection.Find(x => x.UserId == id).ToListAsync();

    public async Task<Goal?> GetAsync(string id) =>
        await _goalsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Goal newGoal) =>
        await _goalsCollection.InsertOneAsync(newGoal);

    public async Task UpdateAsync(string id, Goal updatedGoal) =>
        await _goalsCollection.ReplaceOneAsync(x => x.Id == id, updatedGoal);

    public async Task RemoveAsync(string id) =>
        await _goalsCollection.DeleteOneAsync(x => x.Id == id);
}