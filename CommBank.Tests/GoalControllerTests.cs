using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CommBank.Controllers;
using CommBank.Models;
using CommBank.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommBank.Tests
{
    public class GoalControllerTests
    {
        private readonly Mock<IGoalsService> _mockGoalsService;
        private readonly GoalController _controller;

        public GoalControllerTests()
        {
            // We "Mock" the service so we don't need a real database to run tests
            _mockGoalsService = new Mock<IGoalsService>();
            _controller = new GoalController(_mockGoalsService.Object);
        }

        [Fact]
        public async Task GetGoalsForUser_ReturnsOkResult_WithListOfGoals()
        {
            // 1. ARRANGE
            var userId = "test-user-123";
            var fakeGoals = new List<Goal>
            {
                new Goal { Id = "1", Name = "Holiday", Icon = "✈️", UserId = userId },
                new Goal { Id = "2", Name = "Savings", Icon = "💰", UserId = userId }
            };

            _mockGoalsService.Setup(service => service.GetGoalsForUser(userId))
                             .ReturnsAsync(fakeGoals);

            // 2. ACT
            var result = await _controller.GetGoalsForUser(userId);

            // 3. ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedGoals = Assert.IsType<List<Goal>>(okResult.Value);
            Assert.Equal(2, returnedGoals.Count);
            Assert.Equal("✈️", returnedGoals[0].Icon); // Verifying our new Icon field!
        }

        [Fact]
        public async Task GetGoalsForUser_ReturnsEmptyList_WhenNoGoalsExist()
        {
            // ARRANGE
            var userId = "empty-user";
            _mockGoalsService.Setup(service => service.GetGoalsForUser(userId))
                             .ReturnsAsync(new List<Goal>());

            // ACT
            var result = await _controller.GetGoalsForUser(userId);

            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedGoals = Assert.IsType<List<Goal>>(okResult.Value);
            Assert.Empty(returnedGoals);
        }
    }
}