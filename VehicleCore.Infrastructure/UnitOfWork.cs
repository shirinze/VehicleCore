using VehicleCore.DomainService;
using VehicleCore.DomainService.Repositories;
using VehicleCore.Infrastructure.Data.DBContexts;

namespace VehicleCore.Infrastructure;

public class UnitOfWork(VehicleCoreDbContext db, ICarRepository carRepository,IMotorcycleRepository motorcycleRepository) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await db.SaveChangesAsync(cancellationToken);
    }
    public ICarRepository CarRepository { get; init; } = carRepository;
    public IMotorcycleRepository MotorcycleRepository { get; init; } = motorcycleRepository;

}
