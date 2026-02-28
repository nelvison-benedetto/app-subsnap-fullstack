using MediatR;
using Microsoft.Extensions.Logging;
using SubSnap.Core.Domain.Exceptions;

namespace SubSnap.Application.Common.Behaviors;

/*
 * .application level  exception pipeline. ORA NON DEVI PIU FARE I TRY/CATCH negli handler. gestione centralizzata automatica w plugin MediatR. se un handler lancia un'eccezione, questa viene catturata da questo pipeline behavior, che decide come loggarla e rilanciarla.
 * services.AddTransient(
        typeof(IPipelineBehavior<,>),
        typeof(ExceptionBehavior<,>));
 */
public sealed class ExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;

    public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        try
        {
            return await next();
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex,
                "Domain error for {Request}", typeof(TRequest).Name);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unhandled error for {Request}", typeof(TRequest).Name);

            throw;
        }
    }
}
