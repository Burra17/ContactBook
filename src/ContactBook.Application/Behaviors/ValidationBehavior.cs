using ContactBook.Domain.Shared;
using FluentValidation;
using MediatR;

namespace ContactBook.Application.Behaviors;

// Runs FluentValidation validators before each MediatR request
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : OperationResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        // Run all validators async (useful if validators later need to query the database)
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Count != 0)
        {
            var errorMessage = string.Join("; ", failures.Select(f => f.ErrorMessage));

            // Check if TResponse is a generic OperationResult<T>
            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(OperationResult<>))
            {
                // Extract the type argument (e.g. Guid)
                var resultType = typeof(TResponse).GetGenericArguments()[0];

                // Dynamically build and invoke OperationResult<T>.Failure()
                var failureMethod = typeof(OperationResult<>)
                    .MakeGenericType(resultType)
                    .GetMethod(nameof(OperationResult.Failure), new[] { typeof(string) });

                return (TResponse)failureMethod!.Invoke(null, new object[] { errorMessage })!;
            }

            // For plain OperationResult (without data)
            return (TResponse)(object)OperationResult.Failure(errorMessage);
        }

        return await next();
    }
}
