using CommBank.Models;
using Common.Extension;
using MongoDB.Driver;
using Tag = CommBank.Models.Tag;

namespace CommBank_Server.Services;

public interface IInitService
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    Task Init();
}

public class InitService : IInitService
{
    private readonly IMongoCollection<Goal> _goalsCollection;
    private readonly IMongoCollection<Account> _accountsCollection;
    private readonly IMongoCollection<Tag> _tagsCollection;
    private readonly IMongoCollection<Transaction> _transactionsCollection;
    private readonly IMongoCollection<User> _usersCollection;

    public InitService(IMongoDatabase mongoDatabase)
    {
        _goalsCollection = mongoDatabase.GetCollection<Goal>("Goals");
        _accountsCollection = mongoDatabase.GetCollection<Account>("Accounts");
        _tagsCollection = mongoDatabase.GetCollection<Tag>("Tags");
        _transactionsCollection = mongoDatabase.GetCollection<Transaction>("Transactions");
        _usersCollection = mongoDatabase.GetCollection<User>("Users");
    }

    public async Task Init()
    {
        var tags = new List<Tag>()
        {
            new Tag() { Id = "62a39d27025ca1ba8f1f1c1e", Name = "Groceries" },
            new Tag() { Id = "62a39d42025ca1ba8f1f1c1f", Name = "Restaurant" },
            new Tag() { Id = "62a39d4e025ca1ba8f1f1c20", Name = "Income" },
            new Tag() { Id = "62a39d5a025ca1ba8f1f1c21", Name = "Gas" },
            new Tag() { Id = "62a39d63025ca1ba8f1f1c22", Name = "Investment" },
        };

        await _tagsCollection.InsertManyAsync(tags);

        var transactions = new List<Transaction>()
        {
            new Transaction()
            {
                Id = "62a3a284d07648900df72860", TransactionType = TransactionType.Debit,
                Amount = 135.39,
                DateTime = 1654891140391.ToDateTime(),
                TagIds = new[] { "62a39d27025ca1ba8f1f1c1e" },
                Description = "Whole Foods",
                UserId = "62a29c15f4605c4c9fa7f306"
            },
            new Transaction()
            {
                Id = "62a3a2ded07648900df72861", TransactionType = TransactionType.Debit,
                Amount = 139.26,
                DateTime = 1654027230566.ToDateTime(),
                TagIds = new[] { "62a39d27025ca1ba8f1f1c1e" },
                Description = "Whole Foods",
                UserId = "62a29c15f4605c4c9fa7f306"
            },
            new Transaction()
            {
                Id = "62a3a2ebd07648900df72862", TransactionType = TransactionType.Debit,
                Amount = 26.39,
                DateTime = 1654891243091.ToDateTime(),
                TagIds = new[] { "62a39d42025ca1ba8f1f1c1f" },
                Description = "Chipotle",
                UserId = "62a29c15f4605c4c9fa7f306"
            },
            new Transaction()
            {
                Id = "62a3a348d07648900df7286c", TransactionType = TransactionType.Debit,
                Amount = 1500,
                DateTime = 1654891336929.ToDateTime(),
                TagIds = new[] { "62a39d63025ca1ba8f1f1c22" },
                Description = "Titan",
                UserId = "62a29c15f4605c4c9fa7f306"
            },
            new Transaction()
            {
                Id = "62a3a349d07648900df7286d", TransactionType = TransactionType.Debit,
                Amount = 1500,
                DateTime = 1654027230566.ToDateTime(),
                TagIds = new[] { "62a39d63025ca1ba8f1f1c22" },
                Description = "Titan",
                UserId = "62a29c15f4605c4c9fa7f306"
            }
        };
        await _transactionsCollection.InsertManyAsync(transactions);

        var goalList = new List<Goal>
        {
            new Goal
            {
                Id = "62a3f587102e921da1253d30",
                Name = "House Down Payment",
                TargetAmount = 10000,
                 TargetDate=1736312400000.ToDateTime(),
                Balance = 73501.82,
                 Created=1654912390857.ToDateTime(),
                UserId = "62a29c15f4605c4c9fa7f306",
            },
                new Goal
            {
                Id = "62a3f5e0102e921da1253d33",
                Name = "Tesla Model Y",
                TargetAmount = 60000,
                 TargetDate=1662004800000.ToDateTime(),
                Balance =  43840.02,
                 Created=1654912480950.ToDateTime(),
                UserId = "62a29c15f4605c4c9fa7f306",
            },
                 new Goal
            {
                Id = "62a3f62e102e921da1253d34",
                Name = "Trip to London",
                TargetAmount = 3500,
                 TargetDate=1659412800000.ToDateTime(),
                Balance =   753.89,
                 Created=1654912558236.ToDateTime(),
                UserId = "62a29c15f4605c4c9fa7f306",
            },
                  new Goal
            {
                Id = "62a61945fa15f1cd18516a5f",
                Name = "Trip to NYC",
                TargetAmount = 800,
                 TargetDate=1702184400000.ToDateTime(),
                Balance =  0,
                 Created=1655053065668.ToDateTime(),
                UserId = "62a29c15f4605c4c9fa7f306",
            }
        };
        await _goalsCollection.InsertManyAsync(goalList);

        var account = new Account()
        {
            Id = "62a3e6aad25715026d1a2938",
            Number = 123456789,
            Name = "Tag's Goal Saver",
            Balance = 6483.81,
            AccountType = AccountType.GoalSaver,
            TransactionIds = transactions.Select(t => t.Id).ToList()
        };

        await _accountsCollection.InsertOneAsync(account);

        var user = new User()
        {
            Id = "62a29c15f4605c4c9fa7f306",
            Name = "Tag Ramotar",
            Email = "tag@dropbox.com",
            Password = "$2a$11$10VhY5XIwBeWA4uLIE.sr.c34UvwLRQPD8yy7z/4iiN6ez5z2Pg1S",
            GoalIds = goalList.Select(t => t.Id).ToList(),
            TransactionIds = transactions.Select(t => t.Id).ToList()
        };
        await _usersCollection.InsertOneAsync(user);
    }
}