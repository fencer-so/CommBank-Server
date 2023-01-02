using CommBank.Models;

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
                new()
                {
                    Id = "1",
                    Name = "Tag's GoalSaver"
                },

                new()
                {
                    Id = "2",
                    Name = "Trot's GoalSaver"
                }
            };

            goals = new()
            {
                new()
                {
                    Id = "1",
                    Name = "House Down Payment"
                },

                new()
                {
                    Id = "2",
                    Name = "Tesla Model Y"
                },

                new()
                {
                    Id = "3",
                    Name = "Trip to London"
                },
            };

            tags = new()
            {
                new()
                {
                    Id = "1"
                },

                new()
                {
                    Id = "2"
                }
            };

            transactions = new()
            {
                new()
                {
                    Id = "1"
                },

                new()
                {
                    Id = "2"
                }
            };

            users = new()
            {
                new()
                {
                    Id = "1",
                    Name = "Tag"
                },

                new()
                {
                    Id = "2",
                    Name = "Trot"
                },

                new()
                {
                    Id = "3",
                    Name = "Tugg"
                }
            };
        }

        public List<Account> GetAccounts()
        {
            return accounts;
        }

        public List<Goal> GetGoals()
        {
            return goals;
        }

        public List<Tag> GetTags()
        {
            return tags;
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }

        public List<User> GetUsers()
        {
            return users;
        }
    }
}