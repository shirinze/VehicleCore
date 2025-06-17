using MediatR;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.Application.Commands.Car.Create;

public record CreateCarCommand(string title, GearBoxType gearBox): IRequest;
