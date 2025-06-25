
using MediatR;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.Application.Queries.Car.GetList;

public record GetCarListQuery(string? Q,
    GearBoxType gearBox):IRequest<CarViewModel>;
