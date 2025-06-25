using FluentValidation;
using FluentValidation.Results;
using MediatR;
using VehicleCore.Application.Exceptions;
using VehicleCore.Resources;

namespace VehicleCore.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult =await validators.First().ValidateAsync(context, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = Serialize(validationResult.Errors);
                throw new BadRequestException(Messages.BadRequest, errors);
            }
             
        }
        return await next(cancellationToken);
    }

    private static Dictionary<string, string[]> Serialize(IEnumerable<ValidationFailure> failures)
    {
        var errors = failures.GroupBy(failures => failures.PropertyName).
            ToDictionary
            (
            group => group.Key,
            group => group.Select(failures => failures.ErrorMessage).ToArray()
            );
        return errors;
    }
}
