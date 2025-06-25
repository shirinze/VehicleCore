using MediatR;
using VehicleCore.Application.ViewModels;

namespace VehicleCore.Application.Queries.Car.GetById;

public record GetCarByIdQuery(int Id):IRequest<CarViewModel>;