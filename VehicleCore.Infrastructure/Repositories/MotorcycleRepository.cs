
using Microsoft.EntityFrameworkCore;
using VehicleCore.DomainModel.Models;
using VehicleCore.DomainService.BaseSpecifications;
using VehicleCore.DomainService.Repositories;
using VehicleCore.Infrastructure.Data.DBContexts;
using VehicleCore.Infrastructure.Helpers;

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


    public async Task<(int, List<Motorcycle>)> GetListAsync(BaseSpecification<Motorcycle> specification, CancellationToken cancellationToken)
    {
        var query = set.AsNoTracking().Specify(specification);
        var totalCount =await query.CountAsync(cancellationToken);
        if (specification.IsPaginationEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        var result = await query.ToListAsync(cancellationToken);

        return (totalCount, result);
    }


    public void Update(Motorcycle motorcycle)
    {
        motorcycle.Update();
        set.Update(motorcycle);
    }
}
