using MediatR;

namespace VehicleCore.Application.Commands.Car.Delete;

public record DeleteCarCommand(int Id):IRequest;
