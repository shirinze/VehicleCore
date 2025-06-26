using MediatR;
using VehicleCore.Application.Fatures;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainService.BaseSpecifications;

namespace VehicleCore.Application.Queries.Motorcycle.GetList;

public record GetMotorcycleListQuery
    (
    string? Q,
    OrderType? OrderType,
    int? PageSize,
    int? PageNumber
    ):IRequest<PaginationResult<MotorcycleViewModel>>;
