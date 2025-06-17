using MediatR;
using VehicleCore.Application.Exceptions;
using VehicleCore.DomainService;
using VehicleCore.Resources;

namespace VehicleCore.Application.Commands.Motorcycle.Delete;

public class DeleteMotorcycleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMotorcycleCommand>
{
    public async Task Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await unitOfWork.MotorcycleRepository.GetByIdAsync(request.Id,cancellationToken) ??
            throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Motorcycle), request.Id));
        unitOfWork.MotorcycleRepository.Delete(motorcycle);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
