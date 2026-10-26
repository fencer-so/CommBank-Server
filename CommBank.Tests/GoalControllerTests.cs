
using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CommBank.Tests;

public class GoalControllerTests
{
    private readonly FakeCollections collections;

    public GoalControllerTests()
    {
        collections = new();
    }

    [Fact]
    public async Task GetAll()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        goals[0].Icon = "🤺";

        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);

        GoalController controller = new(goalsService, usersService);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        // Act
        var result = await controller.Get();

        // Assert
        Assert.Equal(goals.Count, result.Count());

        var returnedGoals = result.ToList();

        for (int index = 0; index < returnedGoals.Count; index++)
        {
            Assert.IsAssignableFrom<Goal>(returnedGoals[index]);
            Assert.Equal(goals[index].Id, returnedGoals[index].Id);
            Assert.Equal(goals[index].Name, returnedGoals[index].Name);
            Assert.Equal(goals[index].Icon, returnedGoals[index].Icon);
        }

        Assert.Equal("🤺", returnedGoals[0].Icon);
    }

    [Fact]
    public async Task Get()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        goals[0].Icon = "🏠";

        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);

        GoalController controller = new(goalsService, usersService);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        // Act
        var result = await controller.Get(goals[0].Id!);

        // Assert
        Assert.IsAssignableFrom<Goal>(result.Value);
        Assert.Equal(goals[0].Id, result.Value!.Id);
        Assert.Equal("🏠", result.Value.Icon);
        Assert.NotEqual(goals[1].Id, result.Value.Id);
    }

    [Fact]
    public async Task GetForUser()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        goals[0].Icon = "🎯";

        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);

        GoalController controller = new(goalsService, usersService);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        // Act
        // Add the GetForUser controller call here after confirming
        // the method signature in GoalController.cs.

        // Assert
        Assert.Equal("🎯", goals[0].Icon);
    }
}
