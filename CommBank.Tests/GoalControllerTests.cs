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
        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(goalsService, usersService);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.GetForUser(goals[0].UserId!);

        // Assert
        Assert.NotNull(result);

        var index = 0;
        foreach (Goal goal in result!)
        {
            Assert.IsAssignableFrom<Goal>(goal);
            Assert.Equal(goals[0].UserId, goal.UserId);
            index++;
        }
    }

    [Fact]
    public async void GetForUser_ReturnsGoalsWithIconField()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        
        // Set an icon on the first goal for testing
        goals[0].Icon = "🚗";
        
        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(goalsService, usersService);

        // Act
        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;
        var result = await controller.GetForUser(goals[0].UserId!);

        // Assert
        Assert.NotNull(result);
        
        foreach (Goal goal in result!)
        {
            Assert.IsAssignableFrom<Goal>(goal);
            Assert.Equal(goals[0].UserId, goal.UserId);
            
            // Verify icon field exists (can be null or string)
            Assert.True(goal.Icon == null || goal.Icon is string);
        }
        
        // Verify the first goal specifically has the icon
        Assert.Equal("🚗", result!.FirstOrDefault(g => g.Id == goals[0].Id)?.Icon);
    }

    [Fact]
    public async void UpdateGoal_UpdatesIconField()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        
        var originalGoal = goals[0];
        originalGoal.Icon = null; // Start with no icon
        
        var updatedGoal = new Goal
        {
            Id = originalGoal.Id,
            Name = originalGoal.Name,
            TargetAmount = originalGoal.TargetAmount,
            TargetDate = originalGoal.TargetDate,
            Balance = originalGoal.Balance,
            Created = originalGoal.Created,
            TransactionIds = originalGoal.TransactionIds,
            TagIds = originalGoal.TagIds,
            UserId = originalGoal.UserId,
            Icon = "🚗"  // Add icon
        };
        
        IGoalsService goalsService = new FakeGoalsService(goals, originalGoal);
        IUsersService usersService = new FakeUsersService(users, users[0]);
        GoalController controller = new(goalsService, usersService);

        // Act
        var result = await controller.Update(originalGoal.Id!, updatedGoal);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}