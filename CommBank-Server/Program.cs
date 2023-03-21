using CommBank.Models;
using CommBank.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("CommBank"));
var mongoDatabase = mongoClient.GetDatabase("CommBank");



string accountsJson = System.IO.File.ReadAllText("./Data/Accounts.json");
var accountsDoc = BsonSerializer.Deserialize<List<BsonDocument>>(accountsJson);
var accountsCollection = mongoDatabase.GetCollection<BsonDocument>("Accounts");
foreach(var account in accountsDoc) {
    try {
        await accountsCollection.InsertOneAsync(account);
    }
    catch (Exception e) {
        Console.Write(e.ToString());
    }
    
}

string goalsJson = System.IO.File.ReadAllText("./Data/Goals.json");
var goalsDoc = BsonSerializer.Deserialize<List<BsonDocument>>(goalsJson);
var goalsCollection = mongoDatabase.GetCollection<BsonDocument>("Goals");
foreach (var goal in goalsDoc)
{
    try {
        await goalsCollection.InsertOneAsync(goal);
    } catch (Exception e)
    {
        Console.Write(e.ToString());
    }

}


string tagsJson = System.IO.File.ReadAllText("./Data/Tags.json");
var tagsDoc = BsonSerializer.Deserialize<List<BsonDocument>>(tagsJson);
var tagsCollection = mongoDatabase.GetCollection<BsonDocument>("Tags");
foreach (var tag in tagsDoc)
{
    try
    {
        await tagsCollection.InsertOneAsync(tag);
    } catch (Exception e)
    {
        Console.Write(e.ToString());
    }
}


string transactionsJson = System.IO.File.ReadAllText("./Data/Transactions.json");
var transactionsDoc = BsonSerializer.Deserialize<List<BsonDocument>>(transactionsJson);
var transactionsCollection = mongoDatabase.GetCollection<BsonDocument>("Transactions");
foreach (var transaction in transactionsDoc)
{
    try
    {
        await transactionsCollection.InsertOneAsync(transaction);
    } catch (Exception e)
    {
        Console.Write(e.ToString());
    }
}


string usersJson = System.IO.File.ReadAllText("./Data/Users.json");
var usersDoc = BsonSerializer.Deserialize<List<BsonDocument>>(usersJson);
var usersCollection = mongoDatabase.GetCollection<BsonDocument>("Users");
foreach (var user in usersDoc)
{
    try {
        await usersCollection.InsertOneAsync(user);
    } catch (Exception e)
    {
        Console.Write(e.ToString());
    }

}


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

builder.Services.AddCors();

var app = builder.Build();

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

