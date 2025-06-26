using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using VehicleCore.Application.Fatures;
using VehicleCore.Application.Helpers;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainService;
using VehicleCore.DomainService.Specifications;

namespace VehicleCore.Application.Queries.Car.GetList;

public class GetCarListQueryHandler(IUnitOfWork unitOfWork,IMemoryCache memoryCache) : IRequestHandler<GetCarListQuery, PaginationResult<CarViewModel>>
{
    public async Task<PaginationResult<CarViewModel>> Handle(GetCarListQuery request, CancellationToken cancellationToken)
    {
        var key = $"carlist:{JsonSerializer.Serialize(request)}";
        var paginationResult = memoryCache.Get<PaginationResult<CarViewModel>>(key);
        if(paginationResult is null)
        {
            var specification = new GetCarsByFilterSpecification(request.Q, request.OrderType, request.PageSize, request.PageNumber);
            var (totalcount, cars) =await unitOfWork.CarRepository.GetListAsync(specification, cancellationToken);

            var viewModels = cars.ToViewModel();
            paginationResult = PaginationResult<CarViewModel>.Create(request.PageSize , request.PageNumber, totalcount, viewModels);

            memoryCache.Set(key, paginationResult, TimeSpan.FromSeconds(30));
        }
        return paginationResult;
    }
}
