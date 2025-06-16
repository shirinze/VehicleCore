using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VehicleCore.DomainModel.Attributes;
using VehicleCore.DomainModel.BaseModels;
using VehicleCore.Infrastructure.Data.DBContexts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VehicleCoreDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var enumTypes = typeof(BaseEntity).Assembly
    .GetTypes()
    .Where(t => t.IsEnum && t.GetCustomAttributes(typeof(EnumEndpointAttribute), false).Length != 0)
    .ToList();

//foreach(var enumType in enumTypes)
//{
//    var attribute = (enumType.GetCustomAttributes(typeof(EnumEndpointAttribute)) as EnumEndpointAttribute)!;
//    var route = attribute.Route;
//}

app.Run();
