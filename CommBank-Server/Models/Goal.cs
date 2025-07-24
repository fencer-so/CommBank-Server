using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

/*
This is a data model (also called a POCO – Plain Old CLR Object) representing a
financial goal in the CommBank system.

*/

public class Goal
{
    [BsonId] //marks the property Id as the primary key
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } //Tells the MongoDB driver to treat this string as an ObjectId in the database.

    public string? Name { get; set; }

    public UInt64 TargetAmount { get; set; } = 0;

    public DateTime TargetDate { get; set; }

    public double Balance { get; set; } = 0.00;

    public DateTime Created { get; set; } = DateTime.Now;

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TagIds { get; set; }

    public string? Icon { get; set; } //add an optional public icon of field string type

    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
}