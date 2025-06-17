using MediatR;

namespace VehicleCore.Application.Commands.Car.ToggleActivation;

public record ToggleActivationCarCommand(int Id):IRequest;
