
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using VehicleCore.Application.Exceptions;
using VehicleCore.Application.Helpers;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainService;
using VehicleCore.Resources;

namespace VehicleCore.Application.Queries.Car.GetById;

public class GetCarByIdQueryHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IRequestHandler<GetCarByIdQuery, CarViewModel>
{
    public async Task<CarViewModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var viewModel = memoryCache.Get<CarViewModel>(request.Id);
        if(viewModel is null)
        {
            var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Car), request.Id));
            viewModel = car.ToViewModel();
            memoryCache.Set(request.Id, viewModel, TimeSpan.FromSeconds(30));
        }
        return viewModel;
    }
}
