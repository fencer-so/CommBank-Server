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