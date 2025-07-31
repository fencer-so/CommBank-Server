using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
    // ---------- Arrange ----------
    var users  = collections.GetUsers();
    var goals  = collections.GetGoals();

    var user   = users[0];
    var userId = user.Id!;

    // make every goal belong to this user and give the first an icon
    const string houseEmoji = "🏡";
    for (int i = 0; i < goals.Count; i++)
    {
        goals[i].UserId = userId;
        if (i == 0) goals[i].Icon = houseEmoji;
    }

    IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
    IUsersService usersService = new FakeUsersService(users, user);

    var controller = new GoalController(goalsService, usersService);
    controller.ControllerContext.HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();

    // ---------- Act ----------
    var result = await controller.GetForUser(userId);

    // ---------- Assert ----------
    Assert.NotNull(result);
    Assert.Equal(goals.Count, result!.Count);

    // every goal in the response has the right userId
    Assert.All(result, g => Assert.Equal(userId, g.UserId));

    // first goal kept its icon
    Assert.Equal(houseEmoji, result.First().Icon);
}
}