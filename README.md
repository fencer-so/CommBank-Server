Completed all tasks including MongoDB integration and Goal Model modification.

## Running the server

1. **Set the MongoDB connection string** (required for `dotnet run` to work):
   - Open `CommBank-Server/appsettings.Development.json`.
   - Replace `YOUR_CONNECTION_STRING_HERE` with your real MongoDB connection string in both `ConnectionStrings:CommBank` and `MongoDbSettings:ConnectionString`.
   - Or create `CommBank-Server/Secrets.json` (same folder as `Program.cs`) with:
     ```json
     {
       "ConnectionStrings": {
         "CommBank": "mongodb+srv://user:password@cluster.xxxxx.mongodb.net/?retryWrites=true&w=majority"
       }
     }
     ```
2. From `CommBank-Server` run: `dotnet run`.

**Getting a free connection string:** Sign up at [MongoDB Atlas](https://www.mongodb.com/cloud/atlas), create a free M0 cluster, add a database user, add network access (e.g. `0.0.0.0/0` for testing), then use the “Connect” → “Drivers” connection string.
