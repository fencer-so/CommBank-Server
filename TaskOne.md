{
    "id": "62a3f587102e921da1253d32",
    "name": null,
    "targetAmount": 0,
    "targetDate": "0001-01-01T00:00:00Z",
    "balance": 0,
    "created": "2024-02-13T23:47:07.906Z",
    "icon": "🤺",
    "transactionIds": null,
    "tagIds": null,
    "userId": null
}


// Program.cs

```
var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("CommBank"));
var mongoDatabase = mongoClient.GetDatabase("CommBank");

List<string> dataFiles = new List<string> { "Goals", "Tags", "Users", "Accounts", "Transactions" };
foreach (var dataFile in dataFiles)
{
    DatabaseSeederService.SeedDataFromJson(mongoDatabase, dataFile, $"Data/{dataFile}.json");
}

```

// Services/DatabaseSeederService.cs

```
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

```

// Goal.cs

```
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

public class Goal
{
    ...

    public String? Icon { get; set; }

    ...
}

```

// Postman
{
	"info": {
		"_postman_id": "f15a1525-5897-4e32-b7b1-3c5bf9564d8b",
		"name": "Commbank",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "32956479"
	},
	"item": [
		{
			"name": "GET Goal",
			"request": {
				"auth": {
					"type": "awsv4",
					"awsv4": [
						{
							"key": "service",
							"value": "",
							"type": "string"
						},
						{
							"key": "region",
							"value": "ap-southeast-2",
							"type": "string"
						},
						{
							"key": "secretKey",
							"value": "123",
							"type": "string"
						},
						{
							"key": "accessKey",
							"value": "123",
							"type": "string"
						},
						{
							"key": "addAuthDataToQuery",
							"value": false,
							"type": "boolean"
						}
					]
				},
				"method": "GET",
				"header": [
					{
						"key": "x-api-key",
						"value": "O55F5q03Ha63rd2X2kGug1WwNYcz3wTV4tl7vc3G",
						"type": "default"
					}
				],
				"url": {
					"raw": "http://localhost:5203/api/Goal/62a3f587102e921da1253d32",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5203",
					"path": [
						"api",
						"Goal",
						"62a3f587102e921da1253d32"
					]
				}
			},
			"response": []
		},
		{
			"name": "PUT Icon",
			"request": {
				"method": "PUT",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n  \"icon\": \"🤺\"\r\n}"
				},
				"url": {
					"raw": "http://localhost:5203/api/Goal/62a3f587102e921da1253d32",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5203",
					"path": [
						"api",
						"Goal",
						"62a3f587102e921da1253d32"
					]
				}
			},
			"response": []
		}
	]
}