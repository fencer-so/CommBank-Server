# CommBank Server – Connect & Seed

## 1. Connect server to database

1. Create a [free MongoDB Atlas cluster](https://www.mongodb.com/cloud/atlas/).
2. Create a database user (e.g. under **Database Access**).
3. In Atlas: **Database Deployments** → **Connect** → **Connect your application** → choose **C# / .NET**, copy the connection string.
4. Create `CommBank-Server/Secrets.json` (see `CommBank-Server/Secrets.json.example`):

```json
{
  "ConnectionStrings": {
    "CommBank": "YOUR_ATLAS_CONNECTION_STRING"
  }
}
```

Replace the placeholder with your connection string (and replace `<password>` with the database user password).  
`Secrets.json` is gitignored; do not commit it.

## 2. Seed the database

Seed data is in the `data/` folder (Accounts, Goals, Tags, Transactions, Users).

### Option A: Shell script (MongoDB Database Tools)

Install tools: `brew install mongodb-database-tools` (macOS).

Then run (use the same URI as in Secrets.json):

```bash
export CONNECTION_STRING="mongodb+srv://user:pass@cluster.mongodb.net/..."
./scripts/seed.sh
```

Or: `./scripts/seed.sh "mongodb+srv://..."`

### Option B: MongoDB Compass

1. Connect to your cluster in [Compass](https://www.mongodb.com/products/compass) using your connection string.
2. Create database `CommBank`.
3. For each of `Accounts`, `Goals`, `Tags`, `Transactions`, `Users`: create the collection and use **Add Data** → **Import File** with the corresponding JSON from `data/`.

## 3. Run the API

```bash
cd CommBank-Server
dotnet run
```

Then test with Postman (e.g. goals endpoint). After adding the `Icon` field to the Goal model, responses can include `icon` where present in the data.
