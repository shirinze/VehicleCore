using MediatR;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.Application.Commands.Motorcycle.Update;

public record UpdateMotorcycleCommand(int Id,string title,FuelType fuel):IRequest;