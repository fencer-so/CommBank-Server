using CommBank.Models;
using CommBank.Services;
using MongoDB.Driver;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 1. Ayarları Okuyoruz (appsettings.json içinden alır)
var connectionString = builder.Configuration.GetValue<string>("MongoDB:ConnectionString");
var databaseName = builder.Configuration.GetValue<string>("MongoDB:DatabaseName") ?? "CommBankDB";

// 2. Bağlantı Kontrolü (Fallback) - Şifreyi sildik, güvenli hale getirdik.
if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Contains("{CONNECTION_STRING}"))
{
    // Buraya şifre yazmıyoruz, appsettings dosyasından gelmesini bekliyoruz.
    connectionString = "YOUR_FALLBACK_CONNECTION_STRING_HERE";
}

var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase(databaseName);

// 3. Servislerin Kaydı
builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);
builder.Services.AddSingleton<IAccountsService, AccountsService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IGoalsService, GoalsService>();
builder.Services.AddSingleton<ITagsService, TagsService>();
builder.Services.AddSingleton<ITransactionsService, TransactionsService>();
builder.Services.AddSingleton<IUsersService, UsersService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// 4. Swagger ve Middleware Ayarları
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();