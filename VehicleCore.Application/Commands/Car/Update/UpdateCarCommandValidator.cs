﻿
using FluentValidation;
using VehicleCore.Resources;

namespace VehicleCore.Application.Commands.Car.Update;

public class UpdateCarCommandValidator:AbstractValidator<UpdateCarCommand>
{
    public UpdateCarCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.Car.Title)))
            .MaximumLength(100)
            .WithMessage(string.Format(Messages.MaxLenght, nameof(DomainModel.Models.Car.Title), 100));
    }
}
