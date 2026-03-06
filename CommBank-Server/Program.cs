using System.Text.Json.Serialization;
using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;

// Prefer TLS 1.2 for MongoDB Atlas (helps avoid "TLS alert: InternalError" on Windows)
System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase, allowIntegerValues: false));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

var connectionString = builder.Configuration.GetConnectionString("CommBank");
var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
mongoClientSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(60);
// On Windows, OCSP revocation check can cause TLS "InternalError" handshake failure with Atlas
var ssl = mongoClientSettings.SslSettings ?? new SslSettings();
ssl.CheckCertificateRevocation = false;
mongoClientSettings.SslSettings = ssl;
// Development-only: relax TLS cert validation if Windows still fails (remove in production)
if (builder.Environment.IsDevelopment())
{
    mongoClientSettings.AllowInsecureTls = true;
}
var mongoClient = new MongoClient(mongoClientSettings);
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

