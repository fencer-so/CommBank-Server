using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("CommBank"));
var mongoDatabase = mongoClient.GetDatabase("CommBank");

async Task SeedCollection(string collectionName, string fileName)
{
    var collection = mongoDatabase.GetCollection<MongoDB.Bson.BsonDocument>(collectionName);

    if (await collection.CountDocumentsAsync(new MongoDB.Bson.BsonDocument()) == 0)
    {
        var json = await File.ReadAllTextAsync(Path.Combine(builder.Environment.ContentRootPath, "Data", fileName)); var documents = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<MongoDB.Bson.BsonDocument>>(json);

        Console.WriteLine($"Seeding {collectionName} from {fileName}");

        if (documents != null && documents.Count > 0)
        {
            await collection.InsertManyAsync(documents);
        }
    }
}

await SeedCollection("accounts", "Accounts.json");
await SeedCollection("goals", "Goals.json");
await SeedCollection("tags", "Tags.json");
await SeedCollection("transactions", "Transactions.json");
await SeedCollection("users", "Users.json");


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

