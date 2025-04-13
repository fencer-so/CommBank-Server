using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;
using Microsoft.AspNetCore.Mvc;

namespace CommBank.Tests;

public class GoalControllerTests
{
    private readonly FakeCollections collections;

    public GoalControllerTests()
    {
        collections = new();
    }

    [Fact]
    public async void GetAll()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(goalsService, usersService);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get();

        // Assert
        var index = 0;
        foreach (Goal goal in result)
        {
            Assert.IsAssignableFrom<Goal>(goal);
            Assert.Equal(goals[index].Id, goal.Id);
            Assert.Equal(goals[index].Name, goal.Name);
            index++;
        }
    }

    [Fact]
    public async void Get()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(goalsService, usersService);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get(goals[0].Id!);

        // Assert
        Assert.IsAssignableFrom<Goal>(result.Value);
        Assert.Equal(goals[0], result.Value);
        Assert.NotEqual(goals[1], result.Value);
    }

    [Fact]
    public async void GetForUser()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        
        goals[0].UserId = users[0].Id;  
        goals[1].UserId = users[0].Id;  
        goals[2].UserId = users[1].Id;  
        
        var userGoalService = new CustomFakeGoalsService(goals);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(userGoalService, usersService);
        

        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        
        var resultUser1 = await controller.GetForUser(users[0].Id!);
        
        var resultUser2 = await controller.GetForUser(users[1].Id!);
        
        var resultUser3 = await controller.GetForUser(users[2].Id!);
        
        Assert.NotNull(resultUser1);
        Assert.Equal(2, resultUser1.Count);
        Assert.Contains(resultUser1, g => g.Id == goals[0].Id);
        Assert.Contains(resultUser1, g => g.Id == goals[1].Id);
        Assert.DoesNotContain(resultUser1, g => g.Id == goals[2].Id);
        
        Assert.NotNull(resultUser2);
        Assert.Equal(1, resultUser2.Count);
        Assert.Contains(resultUser2, g => g.Id == goals[2].Id);
        Assert.DoesNotContain(resultUser2, g => g.Id == goals[0].Id);
        Assert.DoesNotContain(resultUser2, g => g.Id == goals[1].Id);
        
        Assert.NotNull(resultUser3);
        Assert.Empty(resultUser3);
    }
}

public class CustomFakeGoalsService : IGoalsService
{
    private readonly List<Goal> _goals;

    public CustomFakeGoalsService(List<Goal> goals)
    {
        _goals = goals;
    }

    public async Task<List<Goal>> GetAsync() =>
        await Task.FromResult(_goals);

    public async Task<List<Goal>?> GetForUserAsync(string userId)
    {
        var userGoals = _goals.Where(g => g.UserId == userId).ToList();
        return await Task.FromResult(userGoals);
    }

    public async Task<Goal?> GetAsync(string id)
    {
        var goal = _goals.FirstOrDefault(g => g.Id == id);
        return await Task.FromResult(goal);
    }

    public async Task CreateAsync(Goal newGoal) =>
        await Task.FromResult(true);

    public async Task UpdateAsync(string id, Goal updatedGoal) =>
        await Task.FromResult(true);

    public async Task RemoveAsync(string id) =>
        await Task.FromResult(true);
}