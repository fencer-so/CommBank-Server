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
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(goalsService, usersService);
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get();
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
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(goalsService, usersService);
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.Get(goals[0].Id!);
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
    IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
    IUsersService usersService = new FakeUsersService(users, users[0]);
    GoalController controller = new(goalsService, usersService);
    // Act
    var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
    controller.ControllerContext.HttpContext = httpContext;
    var result = await controller.GetForUser(users[0].Id!);
    // Assert
    Assert.NotNull(result);
    foreach (Goal goal in result)
    {
        Assert.IsAssignableFrom<Goal>(goal);
    }
}
    [Fact]
    public async void Put()
    {
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(goalsService, usersService);
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        Goal updatedGoal = new()
        {
            Id = goals[0].Id,
            Name = goals[0].Name,
            Icon = "👍",
            UserId = goals[0].UserId,
            TargetAmount = goals[0].TargetAmount,
            TargetDate = goals[0].TargetDate,
            Balance = goals[0].Balance,
            Created = goals[0].Created,
        };
        var result = await controller.Update(goals[0].Id!, updatedGoal);
        Assert.IsAssignableFrom<NoContentResult>(result);
    }
}