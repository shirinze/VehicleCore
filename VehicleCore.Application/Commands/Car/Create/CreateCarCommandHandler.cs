using MediatR;
using VehicleCore.DomainModel.Models;
using VehicleCore.DomainService;

namespace VehicleCore.Application.Commands.Car.Create;

public class CreateCarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCarCommand>
{
    public async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var trackingCode = string.Empty;
        var car = DomainModel.Models.Car.Create(request.title,trackingCode, request.gearBox);
        await unitOfWork.CarRepository.AddAsync(car,cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
     
    }
}
