using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services
{
    public interface IGoalsService
    {
        Task CreateAsync(Goal newGoal);
        Task<List<Goal>> GetAsync();
        Task<List<Goal>?> GetForUserAsync(string id);
        Task<Goal?> GetAsync(string id);
        Task RemoveAsync(string id);
        
        // Updated method signature to support partial updates
        Task UpdateAsync(string id, UpdateDefinition<Goal> updateDefinition);
    }
}
