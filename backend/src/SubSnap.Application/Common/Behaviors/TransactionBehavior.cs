using MediatR;
using SubSnap.Application.Ports.Persistence;

namespace SubSnap.Application.Common.Behaviors;

/*
 * eviti di fare SaveChangesAsync in ogni handler, ma lo fai in un unico punto centrale, con questo pipeline behavior! cosi handler diventa pure orchestration.
 */

public sealed class TransactionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnitOfWork _uow;

    public TransactionBehavior(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        var response = await next(); //handler eseguito, ma non ancora salvato su db

        await _uow.SaveChangesAsync(ct);

        return response;
    }
}