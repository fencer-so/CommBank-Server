using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task GetForUser_ReturnsGoals_ForValidUser()
    {
        // Arrange
        var userId = "507f1f77bcf86cd799439011";

        var mockGoalsService = new Mock<IGoalsService>();
        var mockUsersService = new Mock<IUsersService>();

        var expectedGoals = new List<Goal>
        {
            new Goal
            {
                Id = "507f1f77bcf86cd799439012",
                UserId = userId,
                Name = "Test Goal"
            }
        };

        mockGoalsService
            .Setup(service => service.GetForUserAsync(userId))
            .ReturnsAsync(expectedGoals);

        var controller = new GoalController(
            mockGoalsService.Object,
            mockUsersService.Object
        );

        // Act
        var result = await controller.GetForUser(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Goal", result[0].Name);
    }
}