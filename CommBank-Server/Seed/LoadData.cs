using MongoDB.Bson.Serialization;

namespace CommBank_Server.Seed;

public class LoadData : ILoadData
{
    public async Task<IEnumerable<TEntity>?> LoadDataFromFile<TEntity>(string fileName) where TEntity : class
    {
        var json = await File.ReadAllTextAsync(fileName);

        var data = BsonSerializer.Deserialize<IEnumerable<TEntity>>(json);

        return data;
    }
}
