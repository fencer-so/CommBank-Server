using Microsoft.AspNetCore.Mvc;
using CommBank.Models;
using CommBank.Services;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;
    private readonly ITagsService _tagsService;


    public TransactionController(
        ITransactionsService transactionsService,
        ITagsService tagsService)
    {
        _transactionsService = transactionsService;
        _tagsService = tagsService;
    }



    [HttpGet]
    public async Task<List<Transaction>> Get()
    {
        var transactions = await _transactionsService.GetAsync();


        foreach(var transaction in transactions)
        {
            transaction.Tags = new List<Tag>();


            if(transaction.TagIds != null)
            {
                foreach(var tagId in transaction.TagIds)
                {
                    var tag = await _tagsService.GetAsync(tagId);


                    if(tag != null)
                    {
                        transaction.Tags.Add(tag);
                    }
                }
            }
        }


        return transactions;
    }





    [HttpGet("User/{id:length(24)}")]
    public async Task<List<Transaction>?> GetForUser(string id)
    {
        var transactions =
            await _transactionsService.GetForUserAsync(id);



        if(transactions == null)
            return null;



        foreach(var transaction in transactions)
        {
            transaction.Tags = new List<Tag>();


            if(transaction.TagIds != null)
            {
                foreach(var tagId in transaction.TagIds)
                {
                    var tag = await _tagsService.GetAsync(tagId);


                    if(tag != null)
                    {
                        transaction.Tags.Add(tag);
                    }
                }
            }
        }


        return transactions;
    }





    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Transaction>> Get(string id)
    {
        var transaction =
            await _transactionsService.GetAsync(id);


        if(transaction == null)
            return NotFound();


        return transaction;
    }





    [HttpPost]
    public async Task<IActionResult> Post(Transaction newTransaction)
    {
        await _transactionsService.CreateAsync(newTransaction);

        return CreatedAtAction(
            nameof(Get),
            new { id = newTransaction.Id },
            newTransaction
        );
    }





    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(
        string id,
        Transaction updatedTransaction)
    {
        var transaction =
            await _transactionsService.GetAsync(id);



        if(transaction == null)
            return NotFound();



        updatedTransaction.Id = transaction.Id;


        await _transactionsService.UpdateAsync(
            id,
            updatedTransaction
        );


        return NoContent();
    }





    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var transaction =
            await _transactionsService.GetAsync(id);



        if(transaction == null)
            return NotFound();



        await _transactionsService.RemoveAsync(id);


        return NoContent();
    }
}