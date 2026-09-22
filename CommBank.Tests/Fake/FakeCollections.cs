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
            users = new()
            {
                new()
                {
                    Id = "507f1f77bcf86cd799439011",
                    Name = "Tag"
                },

                new()
                {
                    Id = "507f1f77bcf86cd799439012",
                    Name = "Trot"
                },

                new()
                {
                    Id = "507f1f77bcf86cd799439013",
                    Name = "Tugg"
                }
            };

            accounts = new()
            {
                new()
                {
                    Id = "607f1f77bcf86cd799439021",
                    Name = "Tag's GoalSaver"
                },

                new()
                {
                    Id = "607f1f77bcf86cd799439022",
                    Name = "Trot's GoalSaver"
                }
            };

            goals = new()
            {
                new()
                {
                    Id = "607f1f77bcf86cd799439011",
                    Name = "House Down Payment",
                    UserId = "507f1f77bcf86cd799439011"
                },

                new()
                {
                    Id = "607f1f77bcf86cd799439012",
                    Name = "Tesla Model Y",
                    UserId = "507f1f77bcf86cd799439011"
                },

                new()
                {
                    Id = "607f1f77bcf86cd799439013",
                    Name = "Trip to London",
                    UserId = "507f1f77bcf86cd799439012"
                },
            };

            tags = new()
            {
                new()
                {
                    Id = "707f1f77bcf86cd799439011"
                },

                new()
                {
                    Id = "707f1f77bcf86cd799439012"
                }
            };

            transactions = new()
            {
                new()
                {
                    Id = "807f1f77bcf86cd799439011"
                },

                new()
                {
                    Id = "807f1f77bcf86cd799439012"
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