using Xunit;
using Moq;
using CommBank.Controllers;
using CommBank.Models;
using CommBank.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommBank.Tests
{
    public class GoalsControllerTests
    {
        [Fact]
        public async Task GetGoalsForUser_ReturnsGoals_ForValidUser()
        {
            // ARRANGE
            var userId = "user123";

            var fakeGoals = new List<Goal>
{
    new Goal { Id = "1", Name = "Save for car", Icon = "🚗", UserId = userId },
    new Goal { Id = "2", Name = "Holiday fund", Icon = "✈️", UserId = userId }
};

            var mockService = new Mock<IGoalsService>();
            mockService
                .Setup(s => s.GetForUserAsync(userId))
                .ReturnsAsync(fakeGoals);

            var mockUserService = new Mock<IUsersService>();

var controller = new GoalController(
    mockService.Object,
    mockUserService.Object
);

            

            // ⚠️ IMPORTANT: handle ActionResult
            var result = await controller.GetForUser(userId);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("🚗", result[0].Icon);
        }
    }
}