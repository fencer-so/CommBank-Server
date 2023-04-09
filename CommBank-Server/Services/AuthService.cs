using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public class AuthService : IAuthService
{
    private readonly IMongoCollection<User> _usersCollection;

    public AuthService(IMongoDatabase mongoDatabase)
    {
        _usersCollection = mongoDatabase.GetCollection<User>("Users");
    }

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