using SubSnap.Application.UseCases.Users.RegisterUser;

namespace SubSnap.Application.Ports.Users;

public interface IRUHandler
{
    Task<RUResult> Handle(RUCommand command, CancellationToken ct);
}
