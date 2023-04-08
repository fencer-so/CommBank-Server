namespace CommBank_Server.Seed;

public interface ILoadData
{
    Task<IEnumerable<TEntity>?> LoadDataFromFile<TEntity>(string fileName) where TEntity : class;
}
