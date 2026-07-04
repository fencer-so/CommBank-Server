using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalsService _goalsService;

    public GoalController(IGoalsService goalsService)
    {
        _goalsService = goalsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var goals = await _goalsService.GetAllGoals();
        return Ok(goals);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(int id, [FromBody] Goal goal)
    {
        var updated = await _goalsService.UpdateGoal(id, goal);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }
}