using MediatR;
using VehicleCore.Application.Exceptions;
using VehicleCore.DomainService;
using VehicleCore.Resources;

namespace VehicleCore.Application.Commands.Car.ToggleActivation;

public class ToggleActivationCarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleActivationCarCommand>
{
    public async Task Handle(ToggleActivationCarCommand request, CancellationToken cancellationToken)
    {
        var car=await unitOfWork.CarRepository.GetByIdAsync(request.Id,cancellationToken)??
            throw new NotFoundException(string.Format(Messages.NotFound,nameof(DomainModel.Models.Car),request.Id));
        car.ToggleActivation();
        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
