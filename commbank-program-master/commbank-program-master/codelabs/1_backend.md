# Backend

## Fork rSERVER

- [ ] Fork https://github.com/fencer-so/commbank-server
- [ ] Clone your forked repository

```shell
$ git clone https://github.com/{YOUR_USERNAME}/commbank-server
```

## Create A MongoDB Cluster

- [ ] Register at https://www.mongodb.com/cloud/atlas/register
- [ ] Click the “Create” button
- [ ] Select “Shared” cluster
- [ ] Select AWS as cloud provider
- [ ] Select the recommended region (It will have a star)
- [ ] Click “Create Cluster”

## Create A Database User

- [ ] Navigate to “Database Access”
- [ ] Click “Add New Database User”
- [ ] Select “Password” as authentication method
- [ ] Input username
- [ ] Click “Autogenerate Secure Password”
- [ ] Save the generated password in your password manager (e.g., LastPass)
- [ ] Click “Add User”

## Connect Server With The Database

- [ ] Navigate to “Database Deployments”
- [ ] Click “Connect”
- [ ] Click “Connect Your Application”
- [ ] Select “C# / .NET” for the driver
- [ ] Select “2.13 or later” for the version
- [ ] Copy the connection string
- [ ] Add the connection string to `Secrets.json`

```json
{
    "ConnectionStrings": {
        "CommBank": CONNECTION_STRING
    }
}
```

- [ ] Register the database configuration settings

```cs
// Program.cs

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("CommBank"));
var mongoDatabase = mongoClient.GetDatabase("CommBank");
```

## Add The Goal Model

```cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

public class Goal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }

    public UInt64 TargetAmount { get; set; } = 0;

    public DateTime TargetDate { get; set; }

    public double Balance { get; set; } = 0.00;

    public DateTime Created { get; set; } = DateTime.Now;

    [BsonRepresentation(BsonType.ObjectId)]
    public string? AccountId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TagIds { get; set; }

    public string? Icon { get; set; }
}
```

## Add The Goal Service

### Add The Goal Service Interface

```cs
using CommBank.Models;

namespace CommBank.Services
{
    public interface IGoalsService
    {
        Task CreateAsync(Goal newGoal);
        Task<List<Goal>> GetAsync();
        Task<Goal?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Goal updatedGoal);
    }
}
```

### Add The Goal Service

```cs
using Microsoft.Extensions.Options;
using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public class GoalsService : IGoalsService
{
    private readonly IMongoCollection<Goal> _goalsCollection;

    public GoalsService(IMongoDatabase mongoDatabase)
    {
        _goalsCollection = mongoDatabase.GetCollection<Goal>("Goals");
    }

    public async Task<List<Goal>> GetAsync() =>
        await _goalsCollection.Find(_ => true).ToListAsync();

    public async Task<Goal?> GetAsync(string id) =>
        await _goalsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Goal newGoal) =>
        await _goalsCollection.InsertOneAsync(newGoal);

    public async Task UpdateAsync(string id, Goal updatedGoal) =>
        await _goalsCollection.ReplaceOneAsync(x => x.Id == id, updatedGoal);

    public async Task RemoveAsync(string id) =>
        await _goalsCollection.DeleteOneAsync(x => x.Id == id);
}
```

## Add The Goal Controller

```cs
using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalsService _goalsService;

    public GoalController(IGoalsService goalsService) =>
        _goalsService = goalsService;

    [HttpGet]
    public async Task<List<Goal>> Get() =>
        await _goalsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Goal>> Get(string id)
    {
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        return goal;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Goal newGoal)
    {
        await _goalsService.CreateAsync(newGoal);

        return CreatedAtAction(nameof(Get), new { id = newGoal.Id }, newGoal);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Goal updatedGoal)
    {
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        updatedGoal.Id = goal.Id;

        await _goalsService.UpdateAsync(id, updatedGoal);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        await _goalsService.RemoveAsync(id);

        return NoContent();
    }
}
```

## Inject The Goal Service

```cs
// Program.cs

IGoalsService goalsService = new GoalsService(mongoDatabase);
builder.Services.AddSingleton(goalsService);
```
