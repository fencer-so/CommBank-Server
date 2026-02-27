using CommBank.Models;

namespace CommBank.Services
{
    public interface IUsersService
    {
        Task CreateAsync(User newUser);
        Task<List<User>> GetAsync();
        Task<User?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, User updatedUser);
    }
}