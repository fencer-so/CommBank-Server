using MongoDB.Bson;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

[BsonIgnoreExtraElements]
public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Number")]
    public long? Number { get; set; }

    [BsonElement("Name")]
    public string? Name { get; set; }

    [BsonElement("Balance")]
    public double Balance { get; set; } = 0;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public AccountType AccountType { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }
}