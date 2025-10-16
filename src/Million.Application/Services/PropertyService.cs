using AutoMapper;   
using Million.Application.DTOs;
using Million.Application.Interfaces;

namespace Million.Application.Services; 

public interface IPropertyService
{
    Task<IEnumerable<PropertyDto>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice);
    Task<PropertyDto?> GetPropertyByIdAsync(string id);
}   

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _repo;
    private readonly IMapper _mapper;

    public PropertyService(IPropertyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
    {
        var props = await _repo.GetAllPropertiesAsync(name, address, minPrice, maxPrice);
        return _mapper.Map<IEnumerable<PropertyDto>>(props);
    }

    public async Task<PropertyDto?> GetPropertyByIdAsync(string id)
    {
        var prop = await _repo.GetPropertyByIdAsync(id);
        return property == null ? null : _mapper.Map<PropertyDto>(prop);
    }
}