using MediatR;
using VehicleCore.Application.Exceptions;
using VehicleCore.DomainService;
using VehicleCore.Resources;

namespace VehicleCore.Application.Commands.Car.Update;

public class UpdateCarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCarCommand>
{
    public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car =await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Car), request.Id));

        car.Update(request.Title, request.GearBox);
        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
