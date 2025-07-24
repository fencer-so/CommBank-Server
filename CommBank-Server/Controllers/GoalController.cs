using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;


/*
[Route("api/[controller]")]

This app is an API and the current controller (in this case it is GoalController)
is mapped under [Route("api/[controller]")]

So the endpoint is http://localhost:5203/api/Goal

On this url, it shows this:
[{"id":"62a3f587102e921da1253d32","name":"House Down Payment","targetAmount":100000,"targetDate":"2025-01-08T05:00:00Z","balance":73501.82,"created":"2022-06-11T01:53:10.857Z","transactionIds":null,"tagIds":null,"userId":"62a29c15f4605c4c9fa7f306"},{"id":"62a3f5e0102e921da1253d33","name":"Tesla Model Y","targetAmount":60000,"targetDate":"2022-09-01T04:00:00Z","balance":43840.02,"created":"2022-06-11T01:54:40.95Z","transactionIds":null,"tagIds":null,"userId":"62a29c15f4605c4c9fa7f306"},{"id":"62a3f62e102e921da1253d34","name":"Trip to London","targetAmount":3500,"targetDate":"2022-08-02T04:00:00Z","balance":753.89,"created":"2022-06-11T01:55:58.236Z","transactionIds":null,"tagIds":null,"userId":"62a29c15f4605c4c9fa7f306"},{"id":"62a61945fa15f1cd18516a5f","name":"Trip to NYC","targetAmount":800,"targetDate":"2023-12-10T05:00:00Z","balance":0,"created":"2022-06-12T16:57:45.668Z",
"transactionIds":null,"tagIds":null,"userId":"62a29c15f4605c4c9fa7f306"}]

*/
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