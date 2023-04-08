using CommBank.Models;

namespace CommBank.Services;

public interface ITransactionsService
{
    Task CreateAsync(Transaction newTransaction);
    Task<List<Transaction>> GetAsync();
    Task<List<Transaction>?> GetForUserAsync(string id);
    Task<Transaction?> GetAsync(string id);
    Task RemoveAsync(string id);
    Task UpdateAsync(string id, Transaction updatedTransaction);
}
