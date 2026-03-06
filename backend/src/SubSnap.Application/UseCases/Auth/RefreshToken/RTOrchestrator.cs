using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Application.UseCases.Auth.RefreshToken;

public sealed class RTOrchestrator
{
    private readonly IMediator _mediator;
    public RTOrchestrator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<RTResult> Execute(RTCommand command, CancellationToken ct) {
        return _mediator.Send(command, ct);
    }
}
