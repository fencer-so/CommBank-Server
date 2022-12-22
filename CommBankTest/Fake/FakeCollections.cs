using System;
using CommBank.Models;
using CommBank.Services;

namespace CommBankTest.Fake
{
	public class FakeCollections
	{
		List<Goal> goals;
		List<User> users;

		public FakeCollections()
		{
			this.goals = new()
			{
				new()
				{
					Id = "1",
					Name = "House Down payment"
				},

				new()
				{
					Id = "2",
					Name = "Tesla Model Y"
				},

				new()
				{
					Id = "3",
					Name = "Trip to NYC"
				},

				new()
				{
					Id = "4",
					Name = "Trip to London"
				}

			};

			this.users = new()
			{
				new()
				{
					Id = "1",
					Name = "Tag"
				},

				new()
				{
					Id = "2",
					Name = "Tim"
				},

				new()
				{
					Id = "3",
					Name = "Jake"
				},

				new()
				{
					Id = "4",
					Name = "Jack"
				}
			};
		}

		public List<Goal> GetGoals()
		{
			return this.goals;
		}

		public List<User> GetUsers()
		{
			return this.users;
		}
	}
}

