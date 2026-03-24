using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Load appsettings.json (IMPORTANT FIX)
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// ✅ Get connection string properly
var connectionString = builder.Configuration.GetConnectionString("CommBank");

// ✅ Create MongoDB client
var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase("CommBank");

// Register services
builder.Services.AddSingleton<IAccountsService>(new AccountsService(mongoDatabase));
builder.Services.AddSingleton<IAuthService>(new AuthService(mongoDatabase));
builder.Services.AddSingleton<IGoalsService>(new GoalsService(mongoDatabase));
builder.Services.AddSingleton<ITagsService>(new TagsService(mongoDatabase));
builder.Services.AddSingleton<ITransactionsService>(new TransactionsService(mongoDatabase));
builder.Services.AddSingleton<IUsersService>(new UsersService(mongoDatabase));

// Enable CORS
builder.Services.AddCors();

var app = builder.Build();

// Configure middleware
app.UseCors(policy =>
    policy.AllowAnyOrigin()
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