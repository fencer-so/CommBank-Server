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

        var result = await controller.GetForUser(users[0].Id!);

        // Assert
        Assert.NotNull(result);

        foreach (Goal goal in result!)
        {
            Assert.IsAssignableFrom<Goal>(goal);
            Assert.Equal(users[0].Id, goal.UserId);
        }
    }
    [Fact]
    public async void Update()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        IGoalsService goalsService = new FakeGoalsService(goals, goals[0]);
        IUsersService usersService = new FakeUsersService(users, users[0]);

        GoalController controller = new(goalsService, usersService);

        var updatedGoal = new Goal
        {
            Id = goals[0].Id,
            Name = "Updated Goal",
            UserId = goals[0].UserId,
            Icon = "🎯",
            TargetAmount = goals[0].TargetAmount,
            Balance = goals[0].Balance,
            TargetDate = goals[0].TargetDate,
            Created = goals[0].Created,
            AccountId = goals[0].AccountId,
            TransactionIds = goals[0].TransactionIds,
            TagIds = goals[0].TagIds
        };

        // Act
        var result = await controller.Update(goals[0].Id!, updatedGoal);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void Update_ReturnsNotFound_WhenGoalDoesNotExist()
    {
        // Arrange
        var goals = collections.GetGoals();
        var users = collections.GetUsers();

        IGoalsService goalsService = new FakeGoalsService(goals, null);
        IUsersService usersService = new FakeUsersService(users, users[0]);

        GoalController controller = new(goalsService, usersService);

        var updatedGoal = new Goal();

        // Act
        var result = await controller.Update("123456789012345678901234", updatedGoal);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }