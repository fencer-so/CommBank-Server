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
public async Task GetGoalsForUser_ReturnsOk_WithListOfGoalsForSpecifiedUser()
{
    var testUserId = "user-123";
    var mockGoals = new List<Goal>
    {
        new Goal { Id = "1", Name = "Emergency Fund", TargetAmount = 1000, PublicIcon = "💰" }
    };

    _mockGoalRepository.Setup(repo => repo.GetGoalsByUserIdAsync(testUserId))
                       .ReturnsAsync(mockGoals);

    var result = await _controller.GetGoalsForUser(testUserId);

    var okResult = Assert.IsType<OkObjectResult>(result);
    var returnedGoals = Assert.IsType<List<Goal>>(okResult.Value);
    
    Assert.Single(returnedGoals);
    Assert.Equal("Emergency Fund", returnedGoals[0].Name);
    }
}
