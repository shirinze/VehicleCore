using MediatR;

namespace VehicleCore.Application.Commands.Motorcycle.Delete;

public record DeleteMotorcycleCommand(int Id):IRequest;
