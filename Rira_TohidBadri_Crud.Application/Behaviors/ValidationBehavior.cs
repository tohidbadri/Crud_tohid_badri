
using FluentValidation;
using MediatR;
using Rira_TohidBadri_Crud.Contract.Errors;
using Rira_TohidBadri_Crud.Contract.Exceptions;
namespace Rira_TohidBadri_Crud.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResult = await Task.WhenAll(
            this.validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var failures = validationResult.Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .Select(x => new ValidatorError
            {
                ErrorMessage = x.ErrorMessage,
                Property = x.PropertyName,
            }).ToList();
        if (failures.Any())
        {
            throw new CustomValidationException(failures);
        }

        var response = await next();
        return response;
    }
}

