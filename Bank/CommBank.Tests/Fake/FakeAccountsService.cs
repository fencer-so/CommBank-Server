using Microsoft.Extensions.Options;
using CommBank.Models;
using CommBank.Services;

namespace CommBank.Tests.Fake;

public class FakeAccountsService : IAccountsService
{
    List<Account> _accounts;
    Account _account;

    public FakeAccountsService(List<Account> accounts, Account account)
    {
        _accounts = accounts;
        _account = account;
    }

    public async Task<List<Account>> GetAsync() =>
        await Task.FromResult(_accounts);

    public async Task<Account?> GetAsync(string id) =>
        await Task.FromResult(_account);

    public async Task CreateAsync(Account newAccount) =>
        await Task.FromResult(true);

    public async Task UpdateAsync(string id, Account updatedAccount) =>
        await Task.FromResult(true);

    public async Task RemoveAsync(string id) =>
        await Task.FromResult(true);
}