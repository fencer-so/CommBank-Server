# CommBank Server

A .NET backend server for the Commonwealth Bank Software Engineering virtual experience on Forage.

---

## What This Project Does

A REST API built with ASP.NET Core and MongoDB Atlas. It manages financial data including users, accounts, goals, tags, and transactions.

---

## Setup Steps (What We Did)

### 1. Fork & Clone
Forked the original repo from [fencer-so/commbank-server](https://github.com/fencer-so/commbank-server) and cloned it locally.

### 2. MongoDB Atlas Cluster
- Created a free cluster on [MongoDB Atlas](https://www.mongodb.com/cloud/atlas)
- Created a database user with Atlas Admin privileges
- Whitelisted the local machine's IP under Network Access

### 3. Connect Server to Database
Filled in the MongoDB connection string in `CommBank-Server/Secrets.json` (this file is gitignored — never commit it):

```json
{
  "ConnectionStrings": {
    "CommBank": "mongodb+srv://<username>:<password>@cluster0.xxxxx.mongodb.net/?appName=Cluster0"
  }
}
```

The app reads this in `Program.cs`:
```csharp
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");
var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("CommBank"));
var mongoDatabase = mongoClient.GetDatabase("CommBank");
```

### 4. Seed the Database
Used `mongoimport` to load the seed data from the `data/` folder into MongoDB Atlas:

```bash
mongoimport --uri "mongodb+srv://<user>:<password>@cluster0.xxxxx.mongodb.net/CommBank" --collection Goals --file data/Goals.json --jsonArray
mongoimport --uri "..." --collection Users --file data/Users.json --jsonArray
mongoimport --uri "..." --collection Accounts --file data/Accounts.json --jsonArray
mongoimport --uri "..." --collection Tags --file data/Tags.json --jsonArray
mongoimport --uri "..." --collection Transactions --file data/Transactions.json --jsonArray
```

Collections seeded:
| Collection | Documents |
|---|---|
| Goals | 4 |
| Users | 1 |
| Accounts | 1 |
| Tags | 5 |
| Transactions | 14 |

### 5. Update Target Framework
The project originally targeted `net6.0` which is end-of-life. Updated `CommBank.csproj` to target `net9.0` to run with the installed .NET 9 SDK:

```xml
<TargetFramework>net9.0</TargetFramework>
```

### 6. Install .NET SDK
Since C drive space was limited, installed .NET 9 SDK to `D:\dotnet` using the official install script:

```powershell
Invoke-WebRequest -Uri "https://dot.net/v1/dotnet-install.ps1" -OutFile "D:\dotnet-install.ps1"
& "D:\dotnet-install.ps1" -Channel 9.0 -InstallDir "D:\dotnet"
```

### 7. Run the Server
```bash
D:\dotnet\dotnet run --project CommBank-Server/CommBank.csproj
```

Server runs on:
- `http://localhost:5203`
- `http://localhost:11366`

### 8. Test with Postman (Before Code Change)
`GET http://localhost:5203/api/goal` — returns goals without an `Icon` field.

### 9. Modify the Goal Model
Added an optional `Icon` field to `CommBank-Server/Models/Goal.cs`:

```csharp
public string? Icon { get; set; }
```

### 10. Test with Postman (After Code Change)
`GET http://localhost:5203/api/goal` — returns goals now including the `Icon` field.

---

## Project Structure

```
CommBank-Server/
├── Controllers/        # API route handlers (Goal, User, Account, etc.)
├── Models/             # C# classes mapped to MongoDB documents
├── Services/           # Business logic and MongoDB queries
├── Program.cs          # App entry point, DI setup, MongoDB connection
├── Secrets.json        # Local only — MongoDB connection string (gitignored)
└── CommBank.csproj     # Project config and dependencies

data/                   # Seed data JSON files
├── Goals.json
├── Users.json
├── Accounts.json
├── Tags.json
└── Transactions.json
```

---

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/goal` | Get all goals |
| GET | `/api/goal/{id}` | Get goal by ID |
| GET | `/api/goal/user/{id}` | Get goals for a user |
| POST | `/api/goal` | Create a new goal |
| PUT | `/api/goal/{id}` | Update a goal |
| DELETE | `/api/goal/{id}` | Delete a goal |

Similar CRUD endpoints exist for `/api/user`, `/api/account`, `/api/tag`, `/api/transaction`.

---

## Key Learnings

- How to connect a .NET API to MongoDB Atlas using the MongoDB C# driver
- How to use `mongoimport` to seed a cloud database from JSON files
- How MongoDB document fields map directly to C# model properties — adding a property to the model is all that's needed to expose it in the API response
- How to use `Secrets.json` to keep credentials out of source control
