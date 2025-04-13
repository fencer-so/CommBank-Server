using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
DotNetEnv.Env.Load();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MongoDB client
var connectionString = Environment.GetEnvironmentVariable("connection_string");
var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase("CommBank");

IAccountsService accountsService = new AccountsService(mongoDatabase);
IAuthService authService = new AuthService(mongoDatabase);
IGoalsService goalsService = new GoalsService(mongoDatabase);
ITagsService tagsService = new TagsService(mongoDatabase);
ITransactionsService transactionsService = new TransactionsService(mongoDatabase);
IUsersService usersService = new UsersService(mongoDatabase);

builder.Services.AddSingleton(accountsService);
builder.Services.AddSingleton(authService);
builder.Services.AddSingleton(goalsService);
builder.Services.AddSingleton(tagsService);
builder.Services.AddSingleton(transactionsService);
builder.Services.AddSingleton(usersService);

// Call the async seeding method
try
{
    await SeedDatabaseAsync(mongoDatabase);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred during database seeding: {ex}");
    // Optionally rethrow or handle the exception as needed
    throw; 
}

builder.Services.AddCors();

var app = builder.Build();

// --- Helper method for seeding ---
static async Task SeedDatabaseAsync(IMongoDatabase database)
{
    Console.WriteLine("Starting database seeding...");

    var accountsCollection = database.GetCollection<Account>("Accounts");
    if (await accountsCollection.CountDocumentsAsync(Builders<Account>.Filter.Empty) == 0)
    {
        Console.WriteLine("Seeding Accounts...");
        var accountsJson = await System.IO.File.ReadAllTextAsync("data/Accounts.json");
        var accounts = BsonSerializer.Deserialize<List<Account>>(accountsJson);
        await accountsCollection.InsertManyAsync(accounts);
        Console.WriteLine("Accounts seeded.");
    } else {
        Console.WriteLine("Accounts collection already seeded.");
    }

    var goalsCollection = database.GetCollection<Goal>("Goals");
    if (await goalsCollection.CountDocumentsAsync(Builders<Goal>.Filter.Empty) == 0)
    {
        Console.WriteLine("Seeding Goals...");
        var goalsJson = await System.IO.File.ReadAllTextAsync("data/Goals.json");
        var goals = BsonSerializer.Deserialize<List<Goal>>(goalsJson);
        await goalsCollection.InsertManyAsync(goals);
        Console.WriteLine("Goals seeded.");
    } else {
        Console.WriteLine("Goals collection already seeded.");
    }

    var tagsCollection = database.GetCollection<CommBank.Models.Tag>("Tags");
    if (await tagsCollection.CountDocumentsAsync(Builders<CommBank.Models.Tag>.Filter.Empty) == 0)
    {
        Console.WriteLine("Seeding Tags...");
        var tagsJson = await System.IO.File.ReadAllTextAsync("data/Tags.json");
        var tags = BsonSerializer.Deserialize<List<CommBank.Models.Tag>>(tagsJson);
        await tagsCollection.InsertManyAsync(tags);
        Console.WriteLine("Tags seeded.");
    } else {
        Console.WriteLine("Tags collection already seeded.");
    }
    var transactionsCollection = database.GetCollection<Transaction>("Transactions");
    if (await transactionsCollection.CountDocumentsAsync(Builders<Transaction>.Filter.Empty) == 0)
    {
        Console.WriteLine("Seeding Transactions...");
        var transactionsJson = await System.IO.File.ReadAllTextAsync("data/Transactions.json");
        var transactions = BsonSerializer.Deserialize<List<Transaction>>(transactionsJson);
        await transactionsCollection.InsertManyAsync(transactions);
        Console.WriteLine("Transactions seeded.");
    } else {
        Console.WriteLine("Transactions collection already seeded.");
    }

    var usersCollection = database.GetCollection<User>("Users");
    if (await usersCollection.CountDocumentsAsync(Builders<User>.Filter.Empty) == 0)
    {
        Console.WriteLine("Seeding Users...");
        var usersJson = await System.IO.File.ReadAllTextAsync("data/Users.json");
        var users = BsonSerializer.Deserialize<List<User>>(usersJson);
        await usersCollection.InsertManyAsync(users);
        Console.WriteLine("Users seeded.");
    } else {
        Console.WriteLine("Users collection already seeded.");
    }

    Console.WriteLine("Database seeding finished.");
}

app.UseCors(builder => builder
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
