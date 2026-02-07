using SubSnap.Core.Abstractions.Identity;
using SubSnap.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Infrastructure.Identity.Services;

public class AspNetPasswordHasher : IAspNetPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string plainPassword)
        => _hasher.HashPassword(null!, plainPassword);

    public bool Verify(string plainPassword, string passwordHash)
        => _hasher.VerifyHashedPassword(null!, passwordHash, plainPassword)
           == PasswordVerificationResult.Success;

    public bool Verify(string plainPassword, PasswordHash passwordHash)
    {
        throw new NotImplementedException();
    }
}
