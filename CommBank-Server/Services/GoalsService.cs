using CommBank.Models;
using CommBank.Data;
using MongoDB.Driver;

namespace CommBank.Services
{
    public class GoalsService : IGoalsService
    {
        private readonly IMongoDatabase _database;

        public GoalsService(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task CreateAsync(Goal newGoal)
        {
            var collection = _database.GetCollection<Goal>("goals");
            await collection.InsertOneAsync(newGoal);
            return newGoal;
        }

        public async Task<List<Goal>> GetAsync()
        {
            var collection = _database.GetCollection<Goal>("goals");
            var goals = await collection.FindAsync(_ => true).ToListAsync();
            return goals;
        }

        public async Task<List<Goal>?> GetForUserAsync(string id)
        {
            var collection = _database.GetCollection<Goal>("goals");
            var userGoals = await collection.FindAsync(g => g.UserId == id).ToListAsync();
            return userGoals;
        }

        public async Task<Goal?> GetAsync(string id)
        {
            var collection = _database.GetCollection<Goal>("goals");
            var goal = await collection.FindAsync(g => g.Id == id).FirstOrDefaultAsync();
            return goal;
        }

        public async Task RemoveAsync(string id)
        {
            var collection = _database.GetCollection<Goal>("goals");
            await collection.DeleteOneAsync(g => g.Id == id);
        }

        public async Task UpdateAsync(string id, Goal updatedGoal)
        {
            var collection = _database.GetCollection<Goal>("goals");
            await collection.ReplaceOneAsync(g => g.Id == id, updatedGoal);
        }
    }
}
