using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;

// Simple test to see if the Goal serialization works
var settings = new JsonSerializerSettings
{
    DateFormatString = "yyyy-MM-ddTHH:mm:ssZ",
    DateTimeZoneHandling = DateTimeZoneHandling.Utc
};

var goal = new Goal
{
    Id = ObjectId.GenerateNewId().ToString(),
    Name = "Test Goal",
    TargetAmount = 5000,
    Balance = 1000,
    TargetDate = DateTime.UtcNow.AddMonths(1),
    Created = DateTime.UtcNow,
    UserId = ObjectId.GenerateNewId().ToString()
};

try
{
    var json = JsonConvert.SerializeObject(goal, settings);
    Console.WriteLine("Serialization successful:");
    Console.WriteLine(json);
}
catch (Exception ex)
{
    Console.WriteLine($"Serialization failed: {ex}");
}

public class Goal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }

    public ulong TargetAmount { get; set; } = 0;

    public DateTime TargetDate { get; set; }

    public double Balance { get; set; } = 0.00;

    public DateTime Created { get; set; } = DateTime.UtcNow;

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TagIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
}
