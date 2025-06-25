
using MediatR;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.Application.Commands.Motorcycle.Create;

public record CreateMotorcycleCommand(string Title,FuelType Fuel):IRequest;
