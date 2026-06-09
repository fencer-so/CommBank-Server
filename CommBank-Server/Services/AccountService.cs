using Microsoft.Extensions.Options;
using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

<<<<<<< HEAD
public class AccountsService : IAccountsService
{
    private readonly IMongoCollection<Account> _accountsCollection;

    public AccountsService(IMongoDatabase mongoDatabase)
    {
        _accountsCollection = mongoDatabase.GetCollection<Account>("Accounts");
    }
=======
public class AccountsService(IMongoDatabase mongoDatabase) : IAccountsService
{
    private readonly IMongoCollection<Account> _accountsCollection = mongoDatabase.GetCollection<Account>("Accounts");
>>>>>>> 2bc1eb6 (Your commit message)

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