
using FluentValidation;
using VehicleCore.Resources;

namespace VehicleCore.Application.Commands.Motorcycle.Create;

public class CreateMotorcycleCommandValidator:AbstractValidator<CreateMotorcycleCommand>
{
    public CreateMotorcycleCommandValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty()
           .NotNull()
           .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.Motorcycle.Title)))
           .MaximumLength(100)
           .WithMessage(string.Format(Messages.MaxLenght, nameof(DomainModel.Models.Motorcycle.Title), 100));
    }
}
