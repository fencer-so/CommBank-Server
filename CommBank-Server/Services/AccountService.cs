using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public class AccountsService : IAccountsService
{
    private readonly IMongoCollection<Account> _accountsCollection;

    public AccountsService(IMongoDatabase mongoDatabase)
    {
        _accountsCollection = mongoDatabase.GetCollection<Account>("Accounts");
    }

    public async Task<List<Account>> GetAsync() =>
        await _accountsCollection.Find(_ => true).ToListAsync();

    public async Task<Account?> GetAsync(string id) =>
        await _accountsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Account newAccount) =>
        await _accountsCollection.InsertOneAsync(newAccount);

    public async Task UpdateAsync(string id, Account updatedAccount) =>
        await _accountsCollection.ReplaceOneAsync(x => x.Id == id, updatedAccount);

    public async Task RemoveAsync(string id) =>
        await _accountsCollection.DeleteOneAsync(x => x.Id == id);
}