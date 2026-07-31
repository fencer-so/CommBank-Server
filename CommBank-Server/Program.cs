using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

var mongoSettings = MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("CommBank"));
mongoSettings.SslSettings = new SslSettings
{
    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
};

var mongoClient = new MongoClient(mongoSettings);
var mongoDatabase = mongoClient.GetDatabase("CommBank");

try
{
    // Ping forces an actual connection attempt now, rather than waiting
    // for the first real API request to discover a broken connection.
    mongoDatabase.RunCommand<MongoDB.Bson.BsonDocument>(new MongoDB.Bson.BsonDocument("ping", 1));
    Console.WriteLine("✅ Connected to MongoDB successfully.");
}
catch (Exception ex)
{
    Console.WriteLine("❌ Failed to connect to MongoDB.");
    Console.WriteLine($"Reason: {ex.Message}");
    Console.WriteLine("Check: (1) your connection string in Secrets.json, (2) your Atlas Network Access IP allowlist, (3) your Atlas database user credentials.");
    throw;
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