
using Microsoft.EntityFrameworkCore;
using VehicleCore.DomainModel.Models;
using VehicleCore.DomainService.Repositories;
using VehicleCore.Infrastructure.Data.DBContexts;

namespace VehicleCore.Infrastructure.Repositories;

public class MotorcycleRepository(VehicleCoreDbContext db) : IMotorcycleRepository
{
    private readonly DbSet<Motorcycle> set = db.Set<Motorcycle>();
    public async Task AddAsync(Motorcycle motorcycle, CancellationToken cancellationToken)
    {
        motorcycle.Create();
        await set.AddAsync(motorcycle, cancellationToken);
    }

    public void Delete(Motorcycle motorcycle)
    {
        motorcycle.Delete();
        set.Update(motorcycle);
    }

    public async Task<Motorcycle?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Motorcycle>> GetListAsync(CancellationToken cancellationToken)
    {
        return await set.AsNoTracking().ToListAsync(cancellationToken);
    }

    public void Update(Motorcycle motorcycle)
    {
        motorcycle.Update();
        set.Update(motorcycle);
    }
}
