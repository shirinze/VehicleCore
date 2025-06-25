using FluentValidation;
using VehicleCore.Resources;

namespace VehicleCore.Application.Commands.Car.Create;

public class CreateCarCommandValidator:AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.Car.Title)))
            .MaximumLength(100)
            .WithMessage(string.Format(Messages.MaxLenght, nameof(DomainModel.Models.Car.Title),100));
    }
}
