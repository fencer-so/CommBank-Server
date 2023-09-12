using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? AccountIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? GoalIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }
}