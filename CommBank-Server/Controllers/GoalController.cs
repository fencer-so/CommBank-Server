using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalsService _goalsService;

    public GoalController(IGoalsService goalsService) =>
        _goalsService = goalsService;


    // GET: api/Goal
    [HttpGet]
    public async Task<List<Goal>> Get() =>
        await _goalsService.GetAsync();


    // GET: api/Goal/User/{userId}
[HttpGet("User/{userId:length(24)}")]
public async Task<ActionResult<List<Goal>>> GetGoalsForUser(string userId)
{
    var goals = await _goalsService.GetAsync();

    var userGoals = goals
        .Where(g => g.UserId == userId)
        .ToList();

    if (userGoals.Count == 0)
    {
        return NotFound();
    }

    return userGoals;
}

    // GET: api/Goal/{id}
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


    // POST: api/Goal
    [HttpPost]
    public async Task<IActionResult> Post(Goal newGoal)
    {
        await _goalsService.CreateAsync(newGoal);

        return CreatedAtAction(
            nameof(Get),
            new { id = newGoal.Id },
            newGoal
        );
    }


    // PUT: api/Goal/{id}
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(
        string id,
        Goal updatedGoal)
    {
        Console.WriteLine("PUT RECEIVED");
        Console.WriteLine($"ID: {id}");
        Console.WriteLine($"ICON: {updatedGoal.Icon}");
        Console.WriteLine($"NAME: {updatedGoal.Name}");

        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        updatedGoal.Id = goal.Id;

        await _goalsService.UpdateAsync(
            id,
            updatedGoal
        );

        return NoContent();
    }


    // DELETE: api/Goal/{id}
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