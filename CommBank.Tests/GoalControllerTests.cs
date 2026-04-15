using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommBank.Controllers;
using CommBank.Models;
using CommBank.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommBank.Tests
{
    public class GoalControllerTests
    {
        [Fact]
        public async Task GetGoalsForUser_ReturnsGoals_WhenUserHasGoals()
        {
            // Arrange
            var userId = "user123";
            var mockGoalsService = new Mock<IGoalsService>();
            var mockUsersService = new Mock<IUsersService>();

            var goals = new List<Goal>
            {
                new Goal { Id = "goal1", Name = "Save for holiday", UserId = userId, Amount = 1000, TargetAmount = 5000 },
                new Goal { Id = "goal2", Name = "Emergency fund", UserId = userId, Amount = 500, TargetAmount = 3000 }
            };

            mockGoalsService
                .Setup(s => s.GetAsync(userId))
                .ReturnsAsync(goals);

            var controller = new GoalController(mockGoalsService.Object, mockUsersService.Object);

            // Act
            var result = await controller.GetGoalsForUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedGoals = Assert.IsType<List<Goal>>(okResult.Value);
            Assert.Equal(2, returnedGoals.Count);
        }

        [Fact]
        public async Task GetGoalsForUser_ReturnsEmptyList_WhenUserHasNoGoals()
        {
            // Arrange
            var userId = "userWithNoGoals";
            var mockGoalsService = new Mock<IGoalsService>();
            var mockUsersService = new Mock<IUsersService>();

            mockGoalsService
                .Setup(s => s.GetAsync(userId))
                .ReturnsAsync(new List<Goal>());

            var controller = new GoalController(mockGoalsService.Object, mockUsersService.Object);

            // Act
            var result = await controller.GetGoalsForUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedGoals = Assert.IsType<List<Goal>>(okResult.Value);
            Assert.Empty(returnedGoals);
        }

        [Fact]
        public async Task GetGoalsForUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "nonExistentUser";
            var mockGoalsService = new Mock<IGoalsService>();
            var mockUsersService = new Mock<IUsersService>();

            mockGoalsService
                .Setup(s => s.GetAsync(userId))
                .ReturnsAsync((List<Goal>)null);

            var controller = new GoalController(mockGoalsService.Object, mockUsersService.Object);

            // Act
            var result = await controller.GetGoalsForUser(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
