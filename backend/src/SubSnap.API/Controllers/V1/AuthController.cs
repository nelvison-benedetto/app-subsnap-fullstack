using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubSnap.API.Contracts.Auth;
using SubSnap.API.Contracts.Responses;
using SubSnap.Core.Domain.ValueObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SubSnap.API.Controllers.V1;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthHandler _authHandler;
    public AuthController(AuthHandler authHandler)
    {
        _authHandler = authHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestAuth request, CancellationToken ct)
    {
        var email = new Email(request.Email);  //validation
        var (access, refresh) = await _authHandler.LoginAsync(email, request.Password, ct);
        return Ok(ApiResult<object>.Ok(new
        {
            accessToken = access,
            refreshToken = refresh
        }));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestAuth request, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (userIdClaim is null)
            return Unauthorized();
        var userId = new UserId(Guid.Parse(userIdClaim));
        await _authHandler.LogoutAsync(userId, request.RefreshToken, ct);
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
    [FromBody] RefreshTokenRequestAuth request, CancellationToken ct)
    {
        var (access, refresh) =
            await _authHandler.RefreshAsync(request.RefreshToken, ct);

        return Ok(ApiResult<object>.Ok(new
        {
            accessToken = access,
            refreshToken = refresh
        }));
    }

}