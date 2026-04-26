# CommBank Server

A .NET backend API for the Commonwealth Bank Software Engineering virtual experience on Forage. It handles financial data like users, accounts, goals, tags, and transactions — all stored in MongoDB Atlas.

---

## What's in here

```
CommBank-Server/
├── Controllers/        # API endpoints (Goal, User, Account, Tag, Transaction)
├── Models/             # C# classes that map to MongoDB documents
├── Services/           # Business logic and database queries
├── Program.cs          # App entry point, dependency injection, MongoDB setup
├── Secrets.json        # Your local MongoDB connection string (never committed)
└── CommBank.csproj     # Project config and dependencies

CommBank.Tests/         # xUnit test suite
data/                   # Seed data JSON files
```

---

## Getting started

### 1. Clone the repo

```bash
git clone https://github.com/TimothyYW/CommBank-Server.git
```

### 2. Set up MongoDB Atlas

- Create a free cluster at [mongodb.com/cloud/atlas](https://www.mongodb.com/cloud/atlas)
- Create a database user and whitelist your IP
- Grab your connection string

### 3. Add your connection string

Fill in `CommBank-Server/Secrets.json` — this file is gitignored so your credentials stay local:

```json
{
  "ConnectionStrings": {
    "CommBank": "mongodb+srv://<username>:<password>@cluster0.xxxxx.mongodb.net/?appName=Cluster0"
  }
}
```

### 4. Seed the database

Use `mongoimport` to load the seed data:

```bash
mongoimport --uri "<your-connection-string>/CommBank" --collection Goals --file data/Goals.json --jsonArray
mongoimport --uri "<your-connection-string>/CommBank" --collection Users --file data/Users.json --jsonArray
mongoimport --uri "<your-connection-string>/CommBank" --collection Accounts --file data/Accounts.json --jsonArray
mongoimport --uri "<your-connection-string>/CommBank" --collection Tags --file data/Tags.json --jsonArray
mongoimport --uri "<your-connection-string>/CommBank" --collection Transactions --file data/Transactions.json --jsonArray
```

### 5. Run the server

```bash
dotnet run --project CommBank-Server/CommBank.csproj
```

Server starts on `http://localhost:5203`.

### 6. Run the tests

```bash
dotnet test CommBank.Tests/CommBank.Tests.csproj
```

All 11 tests should pass.

---

## API endpoints

| Method | Endpoint | What it does |
|--------|----------|--------------|
| GET | `/api/goal` | Get all goals |
| GET | `/api/goal/{id}` | Get a goal by ID |
| GET | `/api/goal/user/{id}` | Get all goals for a user |
| POST | `/api/goal` | Create a new goal |
| PUT | `/api/goal/{id}` | Update a goal |
| DELETE | `/api/goal/{id}` | Delete a goal |

Similar CRUD routes exist for `/api/user`, `/api/account`, `/api/tag`, `/api/transaction`.

---

## What changed from the original

- **Added `Icon` field to the `Goal` model** — an optional string field that stores an emoji. The MongoDB driver and controller handle it automatically, no extra wiring needed.
- **Updated target framework** from `net6.0` (end of life) to `net9.0`
- **Added seed data** in the `data/` folder for easy database setup
- **Added `GetForUser` test** to cover the `GET /api/goal/user/{id}` route
- **Gitignored `Secrets.json`** so credentials never accidentally get committed

---

## Notes

- .NET 9 SDK needed — if C drive is full, install to another drive using the [dotnet install script](https://dot.net/v1/dotnet-install.ps1)
- `Secrets.json` is gitignored — never commit it, it contains your database password
