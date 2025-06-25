using MediatR;
using VehicleCore.Application.ViewModels;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.Application.Queries.Motorcycle.GetById;

public record GetMotorcycleByIdQuery(int id):IRequest<MotorcycleViewModel>;
