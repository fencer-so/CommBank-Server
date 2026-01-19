using MongoDB.Driver;
using CommBank.Models;
using System;
using System.Threading.Tasks;

namespace CommBank.Data
{
    public class SeedDatabase
    {
        public static async Task SeedData(IMongoDatabase database)
        {
            Console.WriteLine("Starting database seeding...");
            
            // Get collections
            var usersCollection = database.GetCollection<User>("users");
            var accountsCollection = database.GetCollection<Account>("accounts");
            var goalsCollection = database.GetCollection<Goal>("goals");
            var transactionsCollection = database.GetCollection<Transaction>("transactions");
            var tagsCollection = database.GetCollection<Tag>("tags");
            
            // Clear existing data
            await usersCollection.DeleteManyAsync(FilterDefinition<User>.Empty);
            await accountsCollection.DeleteManyAsync(FilterDefinition<Account>.Empty);
            await goalsCollection.DeleteManyAsync(FilterDefinition<Goal>.Empty);
            await transactionsCollection.DeleteManyAsync(FilterDefinition<Transaction>.Empty);
            await tagsCollection.DeleteManyAsync(FilterDefinition<Tag>.Empty);
            
            Console.WriteLine("Cleared existing data");
            
            // Seed users
            var users = new[]
            {
                new User 
                { 
                    Id = "user123",
                    Username = "john_doe",
                    Email = "john@example.com",
                    Password = "$2a$10$N9qo8kLO2zzq2w3Kv9SjKgqNj", // hashed password
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User 
                { 
                    Id = "user456", 
                    Username = "jane_smith",
                    Email = "jane@example.com",
                    Password = "$2a$10$N9qo8kLO2zzq2w3Kv9SjKgqNj", // hashed password
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            
            await usersCollection.InsertManyAsync(users);
            Console.WriteLine($"Seeded {users.Length} users");
            
            // Seed accounts
            var accounts = new[]
            {
                new Account 
                {
                    Id = "acc123",
                    UserId = "user123",
                    AccountType = "checking",
                    Balance = 1500.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Account 
                {
                    Id = "acc456",
                    UserId = "user456", 
                    AccountType = "savings",
                    Balance = 5000.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            
            await accountsCollection.InsertManyAsync(accounts);
            Console.WriteLine($"Seeded {accounts.Length} accounts");
            
            // Seed goals
            var goals = new[]
            {
                new Goal 
                {
                    Id = "goal123",
                    UserId = "user123",
                    Name = "Emergency Fund",
                    TargetAmount = 10000,
                    TargetDate = new DateTime(2024, 12, 31),
                    Balance = 1500.00,
                    Created = DateTime.UtcNow,
                    TransactionIds = new List<string>(),
                    TagIds = new List<string>(),
                    Icon = "🎯"
                },
                new Goal 
                {
                    Id = "goal456",
                    UserId = "user456",
                    Name = "Vacation Fund",
                    TargetAmount = 3000,
                    TargetDate = new DateTime(2024, 6, 30),
                    Balance = 500.00,
                    Created = DateTime.UtcNow,
                    TransactionIds = new List<string>(),
                    TagIds = new List<string>(),
                    Icon = "✈️"
                }
            };
            
            await goalsCollection.InsertManyAsync(goals);
            Console.WriteLine($"Seeded {goals.Length} goals");
            
            // Seed transactions
            var transactions = new[]
            {
                new Transaction 
                {
                    Id = "trans123",
                    UserId = "user123",
                    Amount = -200.00m,
                    TransactionType = "withdrawal",
                    Description = "ATM withdrawal",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Transaction 
                {
                    Id = "trans456",
                    UserId = "user123", 
                    Amount = 500.00m,
                    TransactionType = "deposit",
                    Description = "Salary deposit",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            
            await transactionsCollection.InsertManyAsync(transactions);
            Console.WriteLine($"Seeded {transactions.Length} transactions");
            
            // Seed tags
            var tags = new[]
            {
                new Tag 
                {
                    Id = "tag123",
                    UserId = "user123",
                    Name = "Bills",
                    Color = "#FF6B6B",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Tag 
                {
                    Id = "tag456",
                    UserId = "user456",
                    Name = "Food",
                    Color = "#4CAF50",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            
            await tagsCollection.InsertManyAsync(tags);
            Console.WriteLine($"Seeded {tags.Length} tags");
            
            Console.WriteLine("Database seeding completed successfully!");
        }
    }
}