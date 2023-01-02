using Microsoft.AspNetCore.Mvc;
using CommBank.Models;
using CommBank.Services;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;

    public TransactionController(ITransactionsService transactionsService) =>
        _transactionsService = transactionsService;

    [HttpGet]
    public async Task<List<Transaction>> Get() =>
        await _transactionsService.GetAsync();

    [HttpGet("User/{id:length(24)}")]
    public async Task<List<Transaction>?> GetForUser(string id) =>
    await _transactionsService.GetForUserAsync(id);

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Transaction>> Get(string id)
    {
        var transaction = await _transactionsService.GetAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        return transaction;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Transaction newTransaction)
    {
        await _transactionsService.CreateAsync(newTransaction);

        return CreatedAtAction(nameof(Get), new { id = newTransaction.Id }, newTransaction);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Transaction updatedTransaction)
    {
        var transaction = await _transactionsService.GetAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        updatedTransaction.Id = transaction.Id;

        await _transactionsService.UpdateAsync(id, updatedTransaction);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var transaction = await _transactionsService.GetAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        await _transactionsService.RemoveAsync(id);

        return NoContent();
    }
}