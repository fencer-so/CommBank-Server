using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace CommBank.Services;

public class DatabaseSeederService 
{
    public static void SeedDataFromJson(IMongoDatabase database, string collectionName, string filePath)
    {
        var collection = database.GetCollection<BsonDocument>(collectionName); 
        var jsonContent = File.ReadAllText(filePath);
        var data = BsonSerializer.Deserialize<List<BsonDocument>>(jsonContent);

        foreach (var obj in data)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", obj.GetValue("_id"));
            var options = new ReplaceOptions { IsUpsert = true };
            collection.ReplaceOne(filter, obj, options);
        }
    }
}