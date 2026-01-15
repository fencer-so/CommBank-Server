using CommBank.Controllers;
using CommBank.Services;
using CommBank.Models;
using CommBank.Tests.Fake;
using Microsoft.AspNetCore.Mvc;
using Xunit;

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
    public async void GetForUser_ReturnsGoalsWithIcons()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        // Add icons to test data
        goals[0].Icon = "🎯";
        goals[1].Icon = "💰";
        goals[2].Icon = null; // Test null icon

        var user = users[0];

        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, user);

        GoalController controller = new(goalsService, usersService);

        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;

        // Act
        var result = await controller.GetForUser(user.Id!);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedGoals = Assert.IsAssignableFrom<IEnumerable<Goal>>(okResult.Value);
        
        Assert.NotNull(returnedGoals);
        Assert.NotEmpty(returnedGoals);
        
        foreach (var goal in returnedGoals)
        {
            Assert.Equal(user.Id, goal.UserId);
           
            Assert.True(goal.Icon == null || !string.IsNullOrEmpty(goal.Icon));
        }
        
        
        var goalList = returnedGoals.ToList();
        Assert.Contains(goalList, g => g.Icon == "🎯");
        Assert.Contains(goalList, g => g.Icon == "💰");
        Assert.Contains(goalList, g => g.Icon == null);
    }

    [Fact]
    public async void GetForUser_UserWithNoGoals_ReturnsEmptyList()
    {
        // Arrange
        var goals = new List<Goal>(); 
        var users = collections.GetUsers();
        
        var user = users[1]; 

        IGoalsService goalsService = new FakeGoalsService(goals, null);
        IUsersService usersService = new FakeUsersService(users, user);

        GoalController controller = new(goalsService, usersService);

        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;

        // Act
        var result = await controller.GetForUser(user.Id!);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedGoals = Assert.IsAssignableFrom<IEnumerable<Goal>>(okResult.Value);
        
        Assert.NotNull(returnedGoals);
        Assert.Empty(returnedGoals);
    }

    [Fact]
    public async void GetForUser_InvalidUserId_ReturnsNotFound()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        
        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, null); // User not found

        GoalController controller = new(goalsService, usersService);

        var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
        controller.ControllerContext.HttpContext = httpContext;

        // Act
        var result = await controller.GetForUser("invalid-user-id");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}