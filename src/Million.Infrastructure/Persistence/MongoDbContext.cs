using Microsoft.Extensions.Options;
using Million.Domain.Entities;
using MongoDB.Driver;

namespace Million.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _database = client.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
}
