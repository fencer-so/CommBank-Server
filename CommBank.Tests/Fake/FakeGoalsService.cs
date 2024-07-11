using Microsoft.Extensions.Options;
using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommBank.Tests.Fake
{
    public class FakeGoalsService : IGoalsService
    {
        private List<Goal> _goals;
        private Goal _goal;

        public FakeGoalsService(List<Goal> goals, Goal goal)
        {
            _goals = goals;
            _goal = goal;
        }

        public async Task<List<Goal>> GetAsync() =>
            await Task.FromResult(_goals);

        public async Task<List<Goal>?> GetForUserAsync(string id) =>
            await Task.FromResult(_goals);

        public async Task<Goal?> GetAsync(string id) =>
            await Task.FromResult(_goal);

        public async Task CreateAsync(Goal newGoal)
        {
            _goals.Add(newGoal);
            await Task.CompletedTask;
        }

        // Update a goal with the given id using the provided update definition
        public async Task UpdateAsync(string id, UpdateDefinition<Goal> updateDefinition)
        {
            var goal = _goals.Find(g => g.Id == id);
            if (goal != null && updateDefinition != null)
            {
                // Simulate the update operation
                // Assume updateDefinition is a Dictionary for simplicity
                var updates = new Dictionary<string, object>
                {
                    { "Name", "Updated Goal Name" },
                    // Add other fields as needed
                };

                foreach (var update in updates)
                {
                    if (update.Key == "Name" && update.Value is string newName)
                    {
                        goal.Name = newName;
                    }
                    // Handle other fields similarly
                }
            }
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(string id)
        {
            var goal = _goals.Find(g => g.Id == id);
            if (goal != null)
            {
                _goals.Remove(goal);
            }
            await Task.CompletedTask;
        }
    }
}
