namespace CommBankTest;
using System;
using CommBank;
using CommBank.Controllers;
using CommBank.Models;
using CommBank.Services;
using Fake; // Contains classes for FakeCollections,
            // FakeGoalService and FakeUserService

public class GoalConttrollerTests
{
    private readonly FakeCollections collections;

    public GoalConttrollerTests()
    {
        collections = new();
    }

    [Fact]
    public async void GetForUser()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();
        IGoalsService goalsService = new FakeGoalService(goals, goals[0]);
        IUsersService usersService = new FakeUserService(users, users[0]);
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
}
