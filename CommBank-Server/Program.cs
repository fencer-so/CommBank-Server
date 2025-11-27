using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("CommBank"));
var database = mongoClient.GetDatabase("CommBank");

IAccountsService accountsService = new AccountsService(database);
IAuthService authService = new AuthService(database);
IGoalsService goalsService = new GoalsService(database);
ITagsService tagsService = new TagsService(database);
ITransactionsService transactionsService = new TransactionsService(database);
IUsersService usersService = new UsersService(database);

builder.Services.AddSingleton(database); // Add mongo database to DI container

builder.Services.AddSingleton(accountsService);
builder.Services.AddSingleton(authService);
builder.Services.AddSingleton(goalsService);
builder.Services.AddSingleton(tagsService);
builder.Services.AddSingleton(transactionsService);
builder.Services.AddSingleton(usersService);

builder.Services.AddCors();

var app = builder.Build();

// Seed database with sample goals if empty
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var mongoDatabase = services.GetRequiredService<IMongoDatabase>();
    var seeder = new CommBank.Seeding.SeedGoals(mongoDatabase);
    seeder.SeedAsync().GetAwaiter().GetResult();
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

