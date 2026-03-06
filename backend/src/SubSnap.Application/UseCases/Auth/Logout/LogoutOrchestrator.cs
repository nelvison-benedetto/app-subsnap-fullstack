using MediatR;

namespace SubSnap.Application.UseCases.Auth.Logout;

public sealed class LogoutOrchestrator
{
    private readonly IMediator _mediator;
    public LogoutOrchestrator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Execute(LogoutCommand command, CancellationToken ct = default) {
        return _mediator.Send(command, ct);
    }
}
