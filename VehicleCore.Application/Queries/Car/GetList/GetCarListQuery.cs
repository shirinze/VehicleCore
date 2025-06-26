
using MediatR;
using VehicleCore.Application.Fatures;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainService.BaseSpecifications;

namespace VehicleCore.Application.Queries.Car.GetList;

public record GetCarListQuery
    (
    string? Q,
    OrderType OrderType,
    int PageSize,
    int PageNumber
    ):IRequest<PaginationResult<CarViewModel>>;
