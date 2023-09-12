using Microsoft.Extensions.Options;
using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public class TransactionsService : ITransactionsService
{
    private readonly IMongoCollection<Transaction> _transactionsCollection;

    public TransactionsService(IMongoDatabase mongoDatabase)
    {
        _transactionsCollection = mongoDatabase.GetCollection<Transaction>("Transactions");
    }

    public async Task<List<Transaction>> GetAsync() =>
        await _transactionsCollection.Find(_ => true).ToListAsync();

    public async Task<List<Transaction>?> GetForUserAsync(string id) =>
        await _transactionsCollection.Find(x => x.UserId == id).ToListAsync();

    public async Task<Transaction?> GetAsync(string id) =>
        await _transactionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Transaction newTransaction) =>
        await _transactionsCollection.InsertOneAsync(newTransaction);

    public async Task UpdateAsync(string id, Transaction updatedTransaction) =>
        await _transactionsCollection.ReplaceOneAsync(x => x.Id == id, updatedTransaction);

    public async Task RemoveAsync(string id) =>
        await _transactionsCollection.DeleteOneAsync(x => x.Id == id);
}