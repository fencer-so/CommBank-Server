using CommBank.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System;

namespace CommBank.Services;

public static class DbSeeder
{
    public static async Task SeedAsync(IMongoDatabase database)
    {
        var usersCollection = database.GetCollection<User>("Users");
        
        // Check if DB is already seeded
        if (await usersCollection.Find(_ => true).AnyAsync()) return;

        var accountsCollection = database.GetCollection<Account>("Accounts");
        var tagsCollection = database.GetCollection<CommBank.Models.Tag>("Tags");
        var transactionsCollection = database.GetCollection<Transaction>("Transactions");
        var goalsCollection = database.GetCollection<Goal>("Goals");

        // TAGS - Create first to reference in transactions
        var tag1 = new CommBank.Models.Tag { Name = "Food" };
        var tag2 = new CommBank.Models.Tag { Name = "Bills" };
        await tagsCollection.InsertManyAsync(new[] { tag1, tag2 });

        // USER
        var user = new User
        {
            Name = "Saniya",
            Email = "saniya@test.com",
            Password = BCrypt.Net.BCrypt.HashPassword("password123"),
            AccountIds = new List<string>(),
            GoalIds = new List<string>(),
            TransactionIds = new List<string>()
        };
        await usersCollection.InsertOneAsync(user);

        // ACCOUNT
        var account = new Account
        {
            Name = "Savings",
            Balance = 10000,
            AccountType = AccountType.NetBankSaver,
            TransactionIds = new List<string>()
        };
        await accountsCollection.InsertOneAsync(account);
        
        // Link Account to User
        user.AccountIds.Add(account.Id!);
        await usersCollection.ReplaceOneAsync(u => u.Id == user.Id, user);

        // TRANSACTIONS
        var transaction1 = new Transaction
        {
            Amount = -300,
            Description = "Dinner",
            UserId = user.Id,
            TagIds = new[] { tag1.Id! },
            DateTime = DateTime.Now,
            TransactionType = TransactionType.Debit
        };
        var transaction2 = new Transaction
        {
            Amount = -1000,
            Description = "Electricity Bill",
            UserId = user.Id,
            TagIds = new[] { tag2.Id! },
            DateTime = DateTime.Now,
            TransactionType = TransactionType.Debit
        };
        await transactionsCollection.InsertManyAsync(new[] { transaction1, transaction2 });
        
        // Link Transactions to User and Account
        user.TransactionIds.Add(transaction1.Id!);
        user.TransactionIds.Add(transaction2.Id!);
        await usersCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
        
        account.TransactionIds.Add(transaction1.Id!);
        account.TransactionIds.Add(transaction2.Id!);
        await accountsCollection.ReplaceOneAsync(a => a.Id == account.Id, account);

        // GOAL
        var goal = new Goal
        {
            Name = "New Phone",
            Icon = "📱",
            TargetAmount = 50000,
            Balance = 10000,
            UserId = user.Id,
            TargetDate = DateTime.Now.AddMonths(6)
        };
        await goalsCollection.InsertOneAsync(goal);
        
        // Link Goal to User
        user.GoalIds.Add(goal.Id!);
        await usersCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
    }
}