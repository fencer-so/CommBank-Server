using Microsoft.Extensions.Options;
using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

<<<<<<< HEAD
public class AuthService : IAuthService
{
    private readonly IMongoCollection<User> _usersCollection;

    public AuthService(IMongoDatabase mongoDatabase)
    {
        _usersCollection = mongoDatabase.GetCollection<User>("Users");
    }
=======
public class AuthService(IMongoDatabase mongoDatabase) : IAuthService
{
    private readonly IMongoCollection<User> _usersCollection = mongoDatabase.GetCollection<User>("Users");
>>>>>>> 2bc1eb6 (Your commit message)

    public async Task<User?> Login(string email, string password)
    {
        var user = await _usersCollection
            .Find(x => x.Email == email)
            .FirstOrDefaultAsync();

        if (user is not null)
        {
            var isCorrectPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (isCorrectPassword)
            {
                return user;
            }
        }

        return null;
    }
}