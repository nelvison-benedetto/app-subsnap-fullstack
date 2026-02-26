using SubSnap.Application.Ports.Auth;
using SubSnap.Core.Domain.Entities;
using SubSnap.Core.Domain.ValueObjects;

namespace SubSnap.Application.UseCases.Auth.Logout.Policies;

public class LogoutTokenPolicy
{
    private readonly IPasswordHasherService _hasher;

    public LogoutTokenPolicy(IPasswordHasherService hasher)
    {
        _hasher = hasher;
    }
    public RefreshToken EnsureValid(string providedToken, User user)
    {
        var token = user.FindActiveRefreshToken(
            storedToken =>
                _hasher.Verify(
                    providedToken,
                    new PasswordHash(storedToken)));

        return token ?? throw new UnauthorizedAccessException();
    }
}
