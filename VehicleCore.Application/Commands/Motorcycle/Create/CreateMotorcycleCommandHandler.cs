﻿using MediatR;
using VehicleCore.DomainService;

namespace VehicleCore.Application.Commands.Motorcycle.Create;

public class CreateMotorcycleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateMotorcycleCommand>
{
    public async Task Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = DomainModel.Models.Motorcycle.Create(request.Title, request.Fuel);
        await unitOfWork.MotorcycleRepository.AddAsync(motorcycle, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
