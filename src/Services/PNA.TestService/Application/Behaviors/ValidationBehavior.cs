using MediatR;

namespace PNA.TestService.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle ( TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken )
    {
        // Add validation logic if needed
        return await next();
    }
}