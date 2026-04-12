using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using CommBank.Models;
using System.IO;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly IMongoDatabase _database;

    public SeedController(IMongoDatabase database)
    {
        _database = database;
    }

    [HttpPost("{collection}")]
    public async Task<IActionResult> Seed(string collection)
    {
        string jsonPath = Path.Combine(@"c:\Users\rasib\Downloads", char.ToUpper(collection[0]) + collection.Substring(1) + ".json");
        
        if (!System.IO.File.Exists(jsonPath))
        {
            return BadRequest($"File not found: {jsonPath}");
        }

        string jsonContent = await System.IO.File.ReadAllTextAsync(jsonPath);
        var data = JsonSerializer.Deserialize<JsonElement>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var coll = _database.GetCollection<BsonDocument>(collection);

        // Clear existing data
        await coll.DeleteManyAsync(new BsonDocument());

        // Insert new
        if (data.ValueKind == JsonValueKind.Array)
        {
            var bsonDocs = new List<BsonDocument>();
            foreach (var item in data.EnumerateArray())
            {
                bsonDocs.Add(BsonDocument.Parse(item.GetRawText()));
            }
            await coll.InsertManyAsync(bsonDocs);
        }

        return Ok($"Seeded {collection} with {data.GetArrayLength()} documents");
    }
}
