﻿using VehicleCore.DomainService.Repositories;

namespace VehicleCore.DomainService;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);

    public ICarRepository CarRepository { get; init; }
    public IMotorcycleRepository MotorcycleRepository { get; init; }
}
