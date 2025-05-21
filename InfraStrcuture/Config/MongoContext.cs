using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Tool> Tools => _database.GetCollection<Tool>("Tools");

}
