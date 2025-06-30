using Azure;
using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using System.Reflection;
using VehicleCore.API.Features;
using VehicleCore.API.Middlewares;
using VehicleCore.Application.Behaviours;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainModel.Attributes;
using VehicleCore.DomainModel.BaseModels;
using VehicleCore.DomainService;
using VehicleCore.DomainService.Proxies;
using VehicleCore.DomainService.Repositories;
using VehicleCore.Infrastructure;
using VehicleCore.Infrastructure.Data.DBContexts;
using VehicleCore.Infrastructure.Proxies;
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

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.AddHttpClient<ITrackingCodeProxy, TrackingCodeProxy>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<Settings>>().Value.TrackingCode;
    client.BaseAddress = new Uri(settings.BaseURL);
})
    .AddPolicyHandler(Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .OrResult(r => !r.IsSuccessStatusCode)
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt),
        onRetry: (response, delay, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} after {delay.TotalSeconds}s due to {response.Exception?.Message ?? response.Result?.StatusCode.ToString()}");
        }));


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

foreach (var enumType in enumTypes)
{
    var attribute = (enumType.GetCustomAttribute(typeof(EnumEndpointAttribute)) as EnumEndpointAttribute)!;
    var route = attribute.Route;

    app.MapGet(route, () =>
    {
        var enumValues = Enum.GetValues(enumType)
                         .Cast<Enum>()
                         .Select(e => new EnumViewModel(e));

        return BaseResult.Success(enumValues);
    })
        .WithTags("Enums");
}

app.Run();
