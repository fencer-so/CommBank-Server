using System;
namespace CommBank_Server.Models
{
	public class CommBankDatabaseSettings : ICommBankDatabaseSettings
	{
        public string AccountsCollectionName { get; set; } = String.Empty;
        public string GoalsCollectionName { get; set; } = String.Empty;
        public string TagsCollectionName { get; set; } = String.Empty;
        public string TransactionsCollectionName { get; set; } = String.Empty;
        public string UsersCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

