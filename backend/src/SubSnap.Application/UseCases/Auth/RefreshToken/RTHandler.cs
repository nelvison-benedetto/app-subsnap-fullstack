using SubSnap.Application.Ports.Auth;
using SubSnap.Application.Ports.Persistence;

namespace SubSnap.Application.UseCases.Auth.RefreshToken;

public sealed class RTHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _uow;

    public RTHandler(
        IUserRepository userRepository,
        IPasswordHasherService passwordHasherService,
        IJwtTokenService jwtTokenService,
        IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
        _jwtTokenService = jwtTokenService;
        _uow = uow;
    }

    public async Task<RefreshTokenResult> Handle(
        RefreshTokenCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.FindByRefreshTokenAsync(
            command.RefreshToken, ct)
            ?? throw new UnauthorizedAccessException();

        var token = user.FindActiveRefreshToken(
            stored =>
                _passwordHasherService.Verify(
                    command.RefreshToken,
                    new PasswordHash(stored)))
            ?? throw new UnauthorizedAccessException();

        user.RevokeRefreshToken(token);

        var newAccess = _jwtTokenService.GenerateAccessToken(user);

        var newRefreshRaw = _jwtTokenService.GenerateRefreshToken();
        var newRefreshHash = _passwordHasherService.Hash(newRefreshRaw);

        user.AddRefreshToken(
            newRefreshHash.Value,
            DateTime.UtcNow.AddDays(30));

        await _uow.SaveChangesAsync(ct);

        return new RefreshTokenResult(newAccess, newRefreshRaw);
    }
}
