using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load .env file ONCE
Env.Load();

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Optional: Keep Secrets.json for fallback
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("Secrets.json", optional: true);

// Get connection string (priority: env var -> Secrets.json -> appsettings.json)
var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI") 
    ?? builder.Configuration.GetConnectionString("CommBank");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("MongoDB connection string is not configured. Set MONGODB_URI environment variable.");
}

var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase("commbank");  // FIXED: Use actual database name

// Register services
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