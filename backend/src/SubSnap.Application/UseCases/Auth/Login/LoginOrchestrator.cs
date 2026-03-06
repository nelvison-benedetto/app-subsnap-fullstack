using MediatR;

namespace SubSnap.Application.UseCases.Auth.Login;

public sealed class LoginOrchestrator
{
    private readonly IMediator _mediator;
    public LoginOrchestrator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<LoginResult> Execute(LoginCommand command, CancellationToken ct = default) {
        return _mediator.Send(command, ct);
    }

}
