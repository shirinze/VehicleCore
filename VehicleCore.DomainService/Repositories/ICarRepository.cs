
using VehicleCore.DomainModel.Models;

namespace VehicleCore.DomainService.Repositories;

public interface ICarRepository
{
    public Task AddAsync(Car car,CancellationToken cancellationToken);
    public void Update(Car car);
    public void Delete(Car car);
    public Task<Car?> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<List<Car>> GetListAsync(CancellationToken cancellationToken);
}
