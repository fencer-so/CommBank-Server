using System;
using CommBank.Services;
using CommBank.Controllers;
using CommBank.Models;
namespace CommBankTest.Fake
{
	public class FakeGoalService : IGoalsService
	{
		List<Goal> goals;
		Goal goal;

		public FakeGoalService(List<Goal> goals, Goal goal)
		{
			this.goals = goals;
			this.goal = goal;
		}

        public async Task<List<Goal>> GetAsync() =>
        await Task.FromResult(this.goals);

        public async Task<List<Goal>?> GetForUserAsync(string id) =>
            await Task.FromResult(this.goals);

        public async Task<Goal?> GetAsync(string id) =>
            await Task.FromResult(this.goal);

        public async Task CreateAsync(Goal newGoal) =>
            await Task.FromResult(true);

        public async Task UpdateAsync(string id, Goal updatedGoal) =>
            await Task.FromResult(true);

        public async Task RemoveAsync(string id) =>
            await Task.FromResult(true);
    }
}

