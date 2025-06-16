
using Microsoft.EntityFrameworkCore;
using VehicleCore.DomainModel.Models;

namespace VehicleCore.Infrastructure.Data.DBContexts;

public class VehicleCoreDbContext(DbContextOptions options):DbContext(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
}
