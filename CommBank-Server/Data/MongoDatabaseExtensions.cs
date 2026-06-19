using System.Reflection;
using CommBank.Data;
using MongoDB.Driver;

namespace CommBank.Data;

public static class MongoDatabaseExtensions
{
    public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database)
    {
        var attribute = typeof(T).GetCustomAttribute<CollectionNameAttribute>();
        var  collection = database.GetCollection<T>(attribute?.Name ?? typeof(T).Name + "s");
        return collection;
    }
}