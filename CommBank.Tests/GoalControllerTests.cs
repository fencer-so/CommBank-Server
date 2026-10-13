
using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CommBank.Tests;

public class GoalControllerTests
{
    private readonly FakeCollections collections;

    public GoalControllerTests()
    {
        collections = new();
    }

    // Test 1: Retrieve all goals
    [Fact]
    public async Task GetAll()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        IGoalsService goalsService =
            new FakeGoalsService(goals, goals[0]);

        IUsersService usersService =
            new FakeUsersService(users, users[0]);

        GoalController controller =
            new(goalsService, usersService);

        controller.ControllerContext.HttpContext =
            new DefaultHttpContext();

        // Act
        var result = await controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(goals.Count, result.Count);

        var index = 0;

        foreach (Goal goal in result)
        {
            Assert.IsAssignableFrom<Goal>(goal);
            Assert.Equal(goals[index].Id, goal.Id);
            Assert.Equal(goals[index].Name, goal.Name);
            index++;
        }
    }

    // Test 2: Retrieve a specific goal
    [Fact]
    public async Task Get()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        IGoalsService goalsService =
            new FakeGoalsService(goals, goals[0]);

        IUsersService usersService =
            new FakeUsersService(users, users[0]);

        GoalController controller =
            new(goalsService, usersService);

        controller.ControllerContext.HttpContext =
            new DefaultHttpContext();

        // Act
        var result = await controller.Get(goals[0].Id!);

        // Assert
        Assert.IsAssignableFrom<Goal>(result.Value);
        Assert.Equal(goals[0], result.Value);
        Assert.NotEqual(goals[1], result.Value);
    }

    // Test 3: Retrieve goals belonging to a user
    [Fact]
    public async Task GetForUser()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        IGoalsService goalsService =
            new FakeGoalsService(goals, goals[0]);

        IUsersService usersService =
            new FakeUsersService(users, users[0]);

        GoalController controller =
            new(goalsService, usersService);

        controller.ControllerContext.HttpContext =
            new DefaultHttpContext();

        string userId = goals[0].UserId!;

        // Act
        var result = await controller.GetForUser(userId);

        // Assert
        Assert.NotNull(result);

        foreach (Goal goal in result!)
        {
            Assert.IsAssignableFrom<Goal>(goal);
            Assert.Equal(userId, goal.UserId);
        }
    }
}
