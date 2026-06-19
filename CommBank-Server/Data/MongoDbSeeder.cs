using System.Reflection;
using System.Text.Json;
using CommBank.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Tag = CommBank.Models.Tag;

namespace CommBank.Data;

public class MongoDbSeeder : IDisposable
{
    
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Account> _accountsCollection;
    private readonly IMongoCollection<Goal> _goalsCollection;
    private readonly IMongoCollection<Tag> _tagsCollection;
    private readonly IMongoCollection<Transaction> _transactionsCollection;
    private readonly IMongoCollection<User> _usersCollection;




    public MongoDbSeeder(IMongoDatabase mongoDatabase)
    {
        _database =  mongoDatabase;
        _accountsCollection = mongoDatabase.GetCollection<Account>();
        _goalsCollection = mongoDatabase.GetCollection<Goal>();
        _tagsCollection = mongoDatabase.GetCollection<Tag>();
        _transactionsCollection = mongoDatabase.GetCollection<Transaction>();
        _usersCollection = mongoDatabase.GetCollection<User>();
    }

    public async Task SeedAsync()
    {
        await SeedCollectionAsync<Account>("Accounts.json");
        await SeedCollectionAsync<Goal>("Goals.json");
        await SeedCollectionAsync<Tag>("Tags.json");
        await SeedCollectionAsync<Transaction>("Transactions.json");
        await SeedCollectionAsync<User>("Users.json");
    }
    
    private async Task SeedCollectionAsync<T>(string fileName)
    {
        var collectionName = typeof(T).GetCustomAttribute<CollectionNameAttribute>()?.Name ?? typeof(T).Name + "s";
        var collection = _database.GetCollection<T>(collectionName);
        
        // Check if the collection is already seeded
        bool hasData = await collection.Find(_ => true).AnyAsync();

        if (!hasData)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed file not found: {filePath}");
                return;
            }

            // Read and deserialize the JSON file
            var jsonString = await File.ReadAllTextAsync(filePath);
            
            var documentArray = BsonSerializer.Deserialize<BsonArray>(jsonString);
            
            var items = documentArray
                .Select(doc => BsonSerializer.Deserialize<T>(doc.AsBsonDocument))
                .ToList();

            // Insert the data if the file wasn't empty
            if (items is { Count: > 0 })
            {
                await collection.InsertManyAsync(items);
                Console.WriteLine($"Successfully seeded {items.Count} items into {collectionName}.");
            }
        }
    }

    public void Dispose()
    {
        
        
    }
}

public static class MongoDbSeederExtensions
{
    public static async Task SeedDataAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var mongoDatabase = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        using var seeder = new MongoDbSeeder(mongoDatabase);
        await seeder.SeedAsync();
    }
}