using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
<<<<<<< HEAD

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("CommBank"));
var mongoDatabase = mongoClient.GetDatabase("CommBank");
=======
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load config
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Secrets.json");

// ✅ CREATE CLIENT FIRST (this was missing)
var mongoClient = new MongoClient(
    builder.Configuration.GetConnectionString("CommBank")
);

var mongoDatabase = mongoClient.GetDatabase("commbank");

// DI registrations
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddSingleton(mongoDatabase);
>>>>>>> 2bc1eb6 (Your commit message)

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

<<<<<<< HEAD
=======

>>>>>>> 2bc1eb6 (Your commit message)
builder.Services.AddCors();

var app = builder.Build();

<<<<<<< HEAD
app.UseCors(builder => builder
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());
=======
app.UseCors(cors => cors
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
>>>>>>> 2bc1eb6 (Your commit message)

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
<<<<<<< HEAD

app.UseAuthorization();

app.MapControllers();

=======
app.UseAuthorization();
app.MapControllers();

// Import data on startup

>>>>>>> 2bc1eb6 (Your commit message)
app.Run();

