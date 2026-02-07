using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SubSnap.Core.Domain.ValueObjects;
using SubSnap.Core.Services.Application;

namespace SubSnap.API.Controllers.V1;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var email = new Email(request.Email);

        var (access, refresh) =
            await _authService.LoginAsync(email, request.Password);

        return Ok(new
        {
            accessToken = access,
            refreshToken = refresh
        });
    }
}
