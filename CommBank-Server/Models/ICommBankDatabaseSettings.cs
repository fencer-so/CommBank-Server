using System;
namespace CommBank_Server.Models
{
	public interface ICommBankDatabaseSettings
	{
		string AccountsCollectionName { get; set; }
        string GoalsCollectionName { get; set; }
		string TagsCollectionName { get; set; }
        string TransactionsCollectionName { get; set; }
		string UsersCollectionName { get; set; }
		string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

