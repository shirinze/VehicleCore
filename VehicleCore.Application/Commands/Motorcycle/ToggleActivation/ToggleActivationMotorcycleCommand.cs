using MediatR;

namespace VehicleCore.Application.Commands.Motorcycle.ToggleActivation;

public record ToggleActivationMotorcycleCommand(int Id):IRequest;

