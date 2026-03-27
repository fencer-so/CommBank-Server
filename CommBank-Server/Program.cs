using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Load configuration (Secrets.json)
builder.Configuration.AddJsonFile("Secrets.json", optional: false, reloadOnChange: true);

// Add controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDB configuration
var connectionString = builder.Configuration.GetConnectionString("CommBank");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("MongoDB connection string is missing.");
}

// Register MongoDB client and database via DI
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(connectionString));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("commbank"));

// Register services (DI-friendly)
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGoalsService, GoalsService>();
builder.Services.AddScoped<ITagsService, TagsService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<IUsersService, UsersService>();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Middleware
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
