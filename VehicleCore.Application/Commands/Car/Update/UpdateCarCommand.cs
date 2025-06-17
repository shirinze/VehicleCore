using MediatR;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.Application.Commands.Car.Update;

public record UpdateCarCommand(int Id,string title,GearBoxType gearBox):IRequest;
