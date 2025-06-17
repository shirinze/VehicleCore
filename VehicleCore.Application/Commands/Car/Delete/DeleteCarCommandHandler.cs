using MediatR;
using VehicleCore.Application.Exceptions;
using VehicleCore.DomainService;
using VehicleCore.Resources;

namespace VehicleCore.Application.Commands.Car.Delete;

public class DeleteCarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCarCommand>
{
    public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Car), request.Id));
        unitOfWork.CarRepository.Delete(car);
        await unitOfWork.CommitAsync(cancellationToken);

    }
}
