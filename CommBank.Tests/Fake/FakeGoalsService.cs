using Microsoft.Extensions.Options;
using CommBank.Models;
using CommBank.Services;

namespace CommBank.Tests.Fake;

<<<<<<< HEAD
public class FakeGoalsService : IGoalsService
{
    List<Goal> _goals;
    Goal _goal;

    public FakeGoalsService(List<Goal> goals, Goal goal)
    {
        _goals = goals;
        _goal = goal;
    }
=======
public class FakeGoalsService(List<Goal> goals, Goal goal) : IGoalsService
{
    List<Goal> _goals = goals;
    Goal _goal = goal;
>>>>>>> 2bc1eb6 (Your commit message)

    public async Task<List<Goal>> GetAsync() =>
        await Task.FromResult(_goals);

    public async Task<List<Goal>?> GetForUserAsync(string id) =>
        await Task.FromResult(_goals);

    public async Task<Goal?> GetAsync(string id) =>
        await Task.FromResult(_goal);

    public async Task CreateAsync(Goal newGoal) =>
        await Task.FromResult(true);

    public async Task UpdateAsync(string id, Goal updatedGoal) =>
        await Task.FromResult(true);

    public async Task RemoveAsync(string id) =>
        await Task.FromResult(true);
}