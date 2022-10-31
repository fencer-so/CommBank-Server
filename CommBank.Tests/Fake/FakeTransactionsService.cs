using CommBank.Models;
using CommBank.Services;

namespace CommBank.Tests.Fake;

public class FakeTransactionsService : ITransactionsService
{
    List<Transaction> _transactions;
    Transaction _transaction;

    public FakeTransactionsService(List<Transaction> transactions, Transaction transaction)
    {
        _transactions = transactions;
        _transaction = transaction;
    }

    public async Task<List<Transaction>> GetAsync() =>
        await Task.FromResult(_transactions);

    public async Task<List<Transaction>?> GetForUserAsync(string id) =>
        await Task.FromResult(_transactions);

    public async Task<Transaction?> GetAsync(string id) =>
        await Task.FromResult(_transaction);

    public async Task CreateAsync(Transaction newTransaction) =>
        await Task.FromResult(true);

    public async Task UpdateAsync(string id, Transaction updatedTransaction) =>
        await Task.FromResult(true);

    public async Task RemoveAsync(string id) =>
        await Task.FromResult(true);
}