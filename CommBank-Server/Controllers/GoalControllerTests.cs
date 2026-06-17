using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

using CommBank.Controllers;
using CommBank.Models;
using CommBank.Services;

namespace CommBank.Tests
{
    public class GoalControllerTests
    {
        private readonly Mock<IGoalsService> _mockGoalsService;
        private readonly Mock<IUsersService> _mockUsersService;
        private readonly GoalController _controller;

        public GoalControllerTests()
        {
            _mockGoalsService = new Mock<IGoalsService>();
            _mockUsersService = new Mock<IUsersService>();

            _controller = new GoalController(
                _mockGoalsService.Object,
                _mockUsersService.Object
            );
        }

        [Fact]
        public async Task GetForUser_ReturnsGoals_WhenDataExists()
        {
            // Arrange
            var userId = "507f1f77bcf86cd799439011";

            var goals = new List<Goal>
            {
                new Goal { Id = "1", UserId = userId },
                new Goal { Id = "2", UserId = userId }
            };

            _mockGoalsService
                .Setup(s => s.GetForUserAsync(userId))
                .ReturnsAsync(goals);

            // Act
            var result = await _controller.GetForUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetForUser_ReturnsEmptyList_WhenNoGoals()
        {
            // Arrange
            var userId = "507f1f77bcf86cd799439011";

            _mockGoalsService
                .Setup(s => s.GetForUserAsync(userId))
                .ReturnsAsync(new List<Goal>());

            // Act
            var result = await _controller.GetForUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetForUser_ServiceCalledOnce()
        {
            // Arrange
            var userId = "507f1f77bcf86cd799439011";

            _mockGoalsService
                .Setup(s => s.GetForUserAsync(userId))
                .ReturnsAsync(new List<Goal>());

            // Act
            await _controller.GetForUser(userId);

            // Assert
            _mockGoalsService.Verify(s => s.GetForUserAsync(userId), Times.Once);
        }
    }
}