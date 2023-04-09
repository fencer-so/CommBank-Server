using CommBank.Models;
using CommBank.Services;

namespace CommBank_Server.Seed;

public class SeedData
{
    private readonly ILoadData _loadData;
    private readonly IAccountsService _accountsService;
    private readonly IGoalsService _goalsService;
    private readonly ITagsService _tagsService;
    private readonly ITransactionsService _transactionsService;
    private readonly IUsersService _usersService;

    public SeedData(ILoadData loadData,
     IAccountsService accountsService,
     IGoalsService goalsService,
     ITagsService tagsService,
     ITransactionsService transactionsService,
     IUsersService usersService)
    {
        _loadData = loadData;
        _accountsService = accountsService;
        _goalsService = goalsService;
        _tagsService = tagsService;
        _transactionsService = transactionsService;
        _usersService = usersService;
    }

    public async Task SeedDatabase()
    {
        var accounts = await _loadData.LoadDataFromFile<Account>("Seed/Accounts.json");
        var goals = await _loadData.LoadDataFromFile<Goal>("Seed/Goals.json");
        var tags = await _loadData.LoadDataFromFile<Tag>("Seed/Tags.json");
        var transactions = await _loadData.LoadDataFromFile<Transaction>("Seed/Transactions.json");
        var users = await _loadData.LoadDataFromFile<User>("Seed/Users.json");

        foreach (var account in accounts)
        {
            await _accountsService.CreateAsync(account);
        }

        foreach (var goal in goals)
        {
            await _goalsService.CreateAsync(goal);
        }

        foreach (var tag in tags)
        {
            await _tagsService.CreateAsync(tag);
        }

        foreach (var transaction in transactions)
        {
            await _transactionsService.CreateAsync(transaction);
        }

        foreach (var user in users)
        {
            await _usersService.CreateAsync(user);
        }
    }
}
