using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using VehicleCore.Application.Fatures;
using VehicleCore.Application.Helpers;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainService;
using VehicleCore.DomainService.Specifications;

namespace VehicleCore.Application.Queries.Motorcycle.GetList;

public class GetMotorcycleListQueryHandler(IUnitOfWork unitOfWork,IMemoryCache memoryCache) : IRequestHandler<GetMotorcycleListQuery, PaginationResult<MotorcycleViewModel>>
{
    public async Task<PaginationResult<MotorcycleViewModel>> Handle(GetMotorcycleListQuery request, CancellationToken cancellationToken)
    {
        var key = $"motorcyclelist:{JsonSerializer.Serialize(request)}";
        var paginationResult = memoryCache.Get<PaginationResult<MotorcycleViewModel>>(key);
        if(paginationResult is null)
        {
            var specification = new GetMotorcycleByFilterSpecification(request.Q,request.OrderType,request.PageSize,request.PageNumber);
            var (totalCount, motorcycles) = await unitOfWork.MotorcycleRepository.GetListAsync(specification, cancellationToken);
            var viewModels = motorcycles.ToViewModel();
            paginationResult = PaginationResult<MotorcycleViewModel>.Create(request.PageSize??0, request.PageNumber??0, totalCount, viewModels);
            memoryCache.Set(key, paginationResult, TimeSpan.FromSeconds(30));
        }
        return paginationResult;
    }
}
