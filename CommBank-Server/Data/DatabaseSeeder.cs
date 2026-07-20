using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CommBank.Data;

public static class DatabaseSeeder
{
    private static readonly (string CollectionName, string[] FileNames)[] SeedSources =
    {
        ("Users", new[] { "Users.json" }),
        ("Accounts", new[] { "Accounts.json" }),
        ("Goals", new[] { "Goals.json" }),
        ("Tags", new[] { "Tags.json" }),
        ("Transactions", new[] { "Transactions.json", "transactions.json" })
    };

    public static async Task SeedAsync(IMongoDatabase mongoDatabase, string contentRootPath, CancellationToken cancellationToken = default)
    {
        var dataDirectory = ResolveDataDirectory(contentRootPath);

        if (dataDirectory is null)
        {
            Console.WriteLine("Seed skipped: data folder was not found.");
            return;
        }

        await EnsureCollectionsExistAsync(mongoDatabase, cancellationToken);

        foreach (var source in SeedSources)
        {
            var seedFilePath = FindExistingFilePath(dataDirectory, source.FileNames);

            if (seedFilePath is null)
            {
                Console.WriteLine($"Seed skipped for {source.CollectionName}: missing JSON file.");
                continue;
            }

            var collection = mongoDatabase.GetCollection<BsonDocument>(source.CollectionName);
            var hasExistingData = await collection.EstimatedDocumentCountAsync(cancellationToken: cancellationToken) > 0;

            if (hasExistingData)
            {
                Console.WriteLine($"Seed skipped for {source.CollectionName}: collection already has data.");
                continue;
            }

            var documents = LoadDocuments(seedFilePath);

            if (documents.Count == 0)
            {
                Console.WriteLine($"Seed skipped for {source.CollectionName}: no documents found in JSON file.");
                continue;
            }

            await collection.InsertManyAsync(documents, cancellationToken: cancellationToken);
            Console.WriteLine($"Seeded {documents.Count} documents into {source.CollectionName}.");
        }
    }

    private static async Task EnsureCollectionsExistAsync(IMongoDatabase mongoDatabase, CancellationToken cancellationToken)
    {
        var existingCollections = await mongoDatabase.ListCollectionNames().ToListAsync(cancellationToken);

        foreach (var source in SeedSources)
        {
            if (existingCollections.Contains(source.CollectionName))
            {
                continue;
            }

            await mongoDatabase.CreateCollectionAsync(source.CollectionName, cancellationToken: cancellationToken);
        }
    }

    private static string? ResolveDataDirectory(string contentRootPath)
    {
        var candidates = new List<string>
        {
            Path.Combine(contentRootPath, "data"),
            Path.GetFullPath(Path.Combine(contentRootPath, "..", "data"))
        };

        var current = new DirectoryInfo(contentRootPath);
        while (current is not null)
        {
            candidates.Add(Path.Combine(current.FullName, "data"));
            current = current.Parent;
        }

        foreach (var candidate in candidates.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            if (!Directory.Exists(candidate))
            {
                continue;
            }

            // Prefer the directory that actually contains one of the expected seed files.
            var hasSeedFiles = SeedSources
                .SelectMany(source => source.FileNames)
                .Any(fileName => File.Exists(Path.Combine(candidate, fileName)));

            if (hasSeedFiles)
            {
                return candidate;
            }
        }

        return null;
    }

    private static string? FindExistingFilePath(string dataDirectory, IEnumerable<string> fileNames)
    {
        foreach (var fileName in fileNames)
        {
            var candidate = Path.Combine(dataDirectory, fileName);
            if (File.Exists(candidate))
            {
                return candidate;
            }
        }

        return null;
    }

    private static List<BsonDocument> LoadDocuments(string seedFilePath)
    {
        var json = File.ReadAllText(seedFilePath);
        var deserialized = BsonSerializer.Deserialize<BsonValue>(json);

        if (deserialized is BsonArray array)
        {
            return array
                .Where(item => item.IsBsonDocument)
                .Select(item => item.AsBsonDocument)
                .ToList();
        }

        if (deserialized is BsonDocument document)
        {
            return new List<BsonDocument> { document };
        }

        return new List<BsonDocument>();
    }
}
