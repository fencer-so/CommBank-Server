using MongoDB.Bson;
using System.Text.Json.Serialization;
using CommBank.Data;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

[CollectionName("Accounts")]
public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public long? Number { get; set; }
    
    public string? Name { get; set; }

    public double Balance { get; set; } = 0;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public AccountType AccountType { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }
}