
using VehicleCore.DomainModel.Models;
using VehicleCore.DomainService.BaseSpecifications;

namespace VehicleCore.DomainService.Repositories;

public interface ICarRepository
{
    public Task AddAsync(Car car,CancellationToken cancellationToken);
    public void Update(Car car);
    public void Delete(Car car);
    public Task<Car?> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<(int,List<Car>)> GetListAsync(BaseSpecification<Car> specification,CancellationToken cancellationToken);
}
