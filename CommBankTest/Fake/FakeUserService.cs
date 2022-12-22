using System;
using CommBank.Services;
using CommBank.Models;

namespace CommBankTest.Fake
{
	public class FakeUserService : IUsersService
	{
		List<User> users;
		User user;

		public FakeUserService(List<User> users, User user)
		{
			this.users = users;
			this.user = user;
		}

        public async Task<List<User>> GetAsync() =>
        await Task.FromResult(this.users);

        public async Task<User?> GetAsync(string id) =>
            await Task.FromResult(this.user);

        public async Task CreateAsync(User newUser) =>
            await Task.FromResult(true);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await Task.FromResult(true);

        public async Task RemoveAsync(string id) =>
            await Task.FromResult(true);
    }
}

