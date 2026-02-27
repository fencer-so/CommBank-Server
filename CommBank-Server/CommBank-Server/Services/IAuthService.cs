using CommBank.Models;

namespace CommBank.Services
{
    public interface IAuthService
    {
        Task<User?> Login(string email, string password);
    }
}