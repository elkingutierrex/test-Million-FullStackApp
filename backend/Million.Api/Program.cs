using Microsoft.Extensions.Options;
using Million.Infrastructure.Persistence;
using Million.Infrastructure.Repositories;
using Million.Application.Services;
using Million.Application.Interfaces;
using AutoMapper;
using Million.Application.DTOs;
using Million.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Config: read session MongoSettings from appsettings
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<MongoDbContext>();

// DI: repos y services
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

// AutoMapper simple
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Property, PropertyDto>().ReverseMap();
});
builder.Services.AddSingleton<IMapper>(sp => mapperConfig.CreateMapper());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
