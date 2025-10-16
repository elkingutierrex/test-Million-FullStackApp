using Million.Application.Interfaces;
using Million.Domain.Entities;
using Million.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Million.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoDbContext _ctx;
    public PropertyRepository(MongoDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Property>> GetAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
    {
        var builder = Builders<Property>.Filter;
        var filter = builder.Empty;

        if (!string.IsNullOrWhiteSpace(name))
            filter &= builder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));

        if (!string.IsNullOrWhiteSpace(address))
            filter &= builder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i"));

        if (minPrice.HasValue)
            filter &= builder.Gte(p => p.Price, minPrice.Value);

        if (maxPrice.HasValue)
            filter &= builder.Lte(p => p.Price, maxPrice.Value);

        return await _ctx.Properties.Find(filter).ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(string id)
    {
        var filter = Builders<Property>.Filter.Eq(p => p.Id, id);
        return await _ctx.Properties.Find(filter).FirstOrDefaultAsync();
    }
}
