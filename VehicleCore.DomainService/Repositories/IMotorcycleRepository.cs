﻿using VehicleCore.DomainModel.Models;
using VehicleCore.DomainService.BaseSpecifications;

namespace VehicleCore.DomainService.Repositories;

public interface IMotorcycleRepository
{
    public Task AddAsync(Motorcycle motorcycle, CancellationToken cancellationToken);
    public void Update(Motorcycle motorcycle);
    public void Delete(Motorcycle motorcycle);
    public Task<Motorcycle?> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<(int,List<Motorcycle>)> GetListAsync(BaseSpecification<Motorcycle> specification,CancellationToken cancellationToken);
}
