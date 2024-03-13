using CommBank.Models;

namespace CommBank.Services
{
    public interface IGoalsService
    {
        Task CreateAsync(Goal newGoal);
        Task<List<Goal>> GetAsync();
        Task<Goal?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Goal updatedGoal);
    }
}