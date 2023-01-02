using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalsService _goalsService;
    private readonly IUsersService _usersService;

    public GoalController(IGoalsService goalsService, IUsersService usersService)
    {
        _goalsService = goalsService;
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<List<Goal>> Get() =>
        await _goalsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Goal>> Get(string id)
    {
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        return goal;
    }

    [HttpGet("User/{id:length(24)}")]
    public async Task<List<Goal>?> GetForUser(string id) =>
        await _goalsService.GetForUserAsync(id);

    [HttpPost]
    public async Task<IActionResult> Post(Goal newGoal)
    {
        await _goalsService.CreateAsync(newGoal);

        if (newGoal.Id is not null && newGoal.UserId is not null)
        {
            var user = await _usersService.GetAsync(newGoal.UserId);

            if (user is not null && user.Id is not null)
            {
                if (user.GoalIds is not null)
                {
                    user.GoalIds.Add(newGoal.Id);
                }
                else
                {
                    user.GoalIds = new()
                    {
                        newGoal.Id
                    };
                }

                await _usersService.UpdateAsync(user.Id, user);
            }
        }

        return CreatedAtAction(nameof(Get), new { id = newGoal.Id }, newGoal);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Goal updatedGoal)
    {
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        updatedGoal.Id = goal.Id;

        await _goalsService.UpdateAsync(id, updatedGoal);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        await _goalsService.RemoveAsync(id);

        return NoContent();
    }
}

public class GoalControllerTests
{
    private readonly FakeCollections collections;

    public GoalControllerTests()
    {
        collections = new();
    }

    // ...

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
}

