using Microsoft.AspNetCore.Mvc;
using Million.Application.Services;

namespace Million.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _service;

    public PropertiesController(IPropertyService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
    {
        var items = await _service.GetPropertiesAsync(name, address, minPrice, maxPrice);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }
}
