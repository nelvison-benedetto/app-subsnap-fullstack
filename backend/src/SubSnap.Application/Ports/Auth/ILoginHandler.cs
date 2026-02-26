using SubSnap.Application.UseCases.Auth.Login;

namespace SubSnap.Application.Ports.Auth;

public interface ILoginHandler
{
    Task<LoginResult> Handle(LoginCommand cmd, CancellationToken ct);
}
