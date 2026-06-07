using CommBank.Models;
using System.Collections.Generic;

namespace CommBank.Tests.Fake
{
    public class FakeCollections
    {
        List<Account> accounts;
        List<Goal> goals;
        List<Tag> tags;
        List<Transaction> transactions;
        List<User> users;

        public FakeCollections()
        {
            accounts = new()
            {
                new Account
                {
                    Id = "1",
                    Name = "Tag's GoalSaver"
                },

                new Account
                {
                    Id = "2",
                    Name = "Trot's GoalSaver"
                }
            };

            goals = new()
{
    new Goal
    {
        Id = "1",
        Name = "House Down Payment",
        UserId = "1"
    },

    new Goal
    {
        Id = "2",
        Name = "Tesla Model Y",
        UserId = "1"
    },

    new Goal
    {
        Id = "3",
        Name = "Trip to London",
        UserId = "2"
    }
};

            tags = new()
            {
                new Tag { Id = "1" },
                new Tag { Id = "2" }
            };

            transactions = new()
            {
                new Transaction { Id = "1" },
                new Transaction { Id = "2" }
            };

            users = new()
            {
                new User
                {
                    Id = "1",
                    Name = "Tag"
                },

                new User
                {
                    Id = "2",
                    Name = "Trot"
                },

                new User
                {
                    Id = "3",
                    Name = "Tugg"
                }
            };
        }

        public List<Account> GetAccounts() => accounts;
        public List<Goal> GetGoals() => goals;
        public List<Tag> GetTags() => tags;
        public List<Transaction> GetTransactions() => transactions;
        public List<User> GetUsers() => users;
    }
}