using FluentValidation;
using FluentValidation.Results;
using Mediator;

namespace KibernumCrud.Application.Configuration;

public sealed class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ValidationFailure
{
    public async ValueTask<TResponse> Handle(
        TRequest message,
        MessageHandlerDelegate<TRequest, TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validator is null)
        {
            return await next(message, cancellationToken);
        }
        
        ValidationResult? validationResult = await validator.ValidateAsync(message, cancellationToken);
        
        if (validationResult.IsValid)
        {
            return await next(message, cancellationToken);
        }
        
        List<ValidationFailure> errors = validationResult.Errors.ToList();

        return (dynamic)errors;
    }
}