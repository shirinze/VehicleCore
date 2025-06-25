using MediatR;
using Microsoft.Extensions.Caching.Memory;
using VehicleCore.Application.Exceptions;
using VehicleCore.Application.Helpers;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainService;
using VehicleCore.Resources;

namespace VehicleCore.Application.Queries.Motorcycle.GetById;

public class GetMotorcycleByIdQueryHandler(IUnitOfWork unitOfWork,IMemoryCache memoryCache) :
    IRequestHandler<GetMotorcycleByIdQuery, MotorcycleViewModel>
{
    public async Task<MotorcycleViewModel> Handle(GetMotorcycleByIdQuery request, CancellationToken cancellationToken)
    {
        var viewModel = memoryCache.Get<MotorcycleViewModel>(request.id);
        if(viewModel is null)
        {
            var motorcycle = await unitOfWork.MotorcycleRepository.GetByIdAsync(request.id, cancellationToken) ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Motorcycle), request.id));
            viewModel = motorcycle.ToViewModel();
            memoryCache.Set(request.id, viewModel, TimeSpan.FromSeconds(30));
        }
        return viewModel;
    }
}
