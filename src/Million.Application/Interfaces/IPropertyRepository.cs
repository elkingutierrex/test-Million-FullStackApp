using      Million.Domain.Entities;

namespace Million.Application.Interfaces;  

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice);
    Task<Property?> GetPropertyByIdAsync(string id);
    // Task AddPropertyAsync(Property property);
    // Task UpdatePropertyAsync(Property property);
    // Task DeletePropertyAsync(string id);
}