using MediatR;
using VehicleCore.Application.Exceptions;
using VehicleCore.DomainService;
using VehicleCore.Resources;

namespace VehicleCore.Application.Commands.Motorcycle.ToggleActivation;

public class ToggleActivationMotorcycleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleActivationMotorcycleCommand>
{
    public async Task Handle(ToggleActivationMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await unitOfWork.MotorcycleRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Motorcycle), request.Id));
        motorcycle.ToggleActivation();
        unitOfWork.MotorcycleRepository.Update(motorcycle);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
