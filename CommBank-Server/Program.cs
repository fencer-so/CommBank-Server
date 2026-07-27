using MongoDB.Driver;

try
{
   var connectionString =
    "mongodb+srv://shaikshah:Test123456@cluster0.8uzhwc1.mongodb.net/?appName=Cluster0";

    var settings = MongoClientSettings.FromConnectionString(connectionString);
    settings.ServerApi = new ServerApi(ServerApiVersion.V1);

    var client = new MongoClient(settings);

    var dbs = client.ListDatabaseNames().ToList();

    Console.WriteLine("SUCCESS!");

    foreach (var db in dbs)
    {
        Console.WriteLine(db);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}