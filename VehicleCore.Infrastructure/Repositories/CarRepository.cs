
using Microsoft.EntityFrameworkCore;
using VehicleCore.DomainModel.Models;
using VehicleCore.DomainService.Repositories;
using VehicleCore.Infrastructure.Data.DBContexts;

namespace VehicleCore.Infrastructure.Repositories;

public class CarRepository(VehicleCoreDbContext db) : ICarRepository
{
    private readonly DbSet<Car> set = db.Set<Car>();
    public async Task AddAsync(Car car, CancellationToken cancellationToken)
    {
        car.Create();
        await set.AddAsync(car, cancellationToken);
    }

    public void Delete(Car car)
    {
        car.Delete();
        set.Update(car);
    }

    public async Task<Car?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<List<Car>> GetListAsync(CancellationToken cancellationToken)
    {
        return await set.AsNoTracking().ToListAsync(cancellationToken);
    }

    public void Update(Car car)
    {
        car.Update();
        set.Update(car);
    }
}
