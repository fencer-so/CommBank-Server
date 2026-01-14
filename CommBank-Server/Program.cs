using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

    Console.WriteLine("Attempting to connect to MongoDB...");
    var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("CommBank"));
    // Use the actual Atlas database name as shown in the cluster
    var mongoDatabase = mongoClient.GetDatabase("commbank");
    Console.WriteLine("MongoDB connected to database: commbank");

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
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseHttpsRedirection();
    }

    app.UseAuthorization();

    app.MapControllers();

    // Add a simple health check endpoint
    app.MapGet("/health", () => "OK");

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Fatal error: {ex}");
    throw;
}

