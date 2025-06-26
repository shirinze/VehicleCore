using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleCore.API.Middlewares;
using VehicleCore.Application.Behaviours;
using VehicleCore.DomainModel.Attributes;
using VehicleCore.DomainModel.BaseModels;
using VehicleCore.DomainService;
using VehicleCore.DomainService.Repositories;
using VehicleCore.Infrastructure;
using VehicleCore.Infrastructure.Data.DBContexts;
using VehicleCore.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VehicleCoreDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
    options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);


builder.Services.AddMemoryCache();

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

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();

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
