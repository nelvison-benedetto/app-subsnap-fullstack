using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.DTOs.Auth;

public sealed class LoginRequest
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
