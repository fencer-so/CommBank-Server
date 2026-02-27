using CommBank.Models;
using CommBank.Services;

namespace CommBank.Tests.Fake;

public class FakeUsersService : IUsersService
{
    List<User> _users;
    User _user;

    public FakeUsersService(List<User> users, User user)
    {
        _users = users;
        _user = user;
    }

    public async Task<List<User>> GetAsync() =>
        await Task.FromResult(_users);

    public async Task<User?> GetAsync(string id) =>
        await Task.FromResult(_user);

    public async Task CreateAsync(User newUser) =>
        await Task.FromResult(true);

    public async Task UpdateAsync(string id, User updatedUser) =>
        await Task.FromResult(true);

    public async Task RemoveAsync(string id) =>
        await Task.FromResult(true);
}