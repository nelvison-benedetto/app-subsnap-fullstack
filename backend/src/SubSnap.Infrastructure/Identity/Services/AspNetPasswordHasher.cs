using SubSnap.Core.Abstractions.Identity;
using SubSnap.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Infrastructure.Identity.Services;

public class AspNetPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public PasswordHash Hash(string plainPassword)
    {
        var hash = _hasher.HashPassword(null!, plainPassword);
        return new PasswordHash(hash);
    }

    public bool Verify(string plainPassword, PasswordHash passwordHash)
    {
        var result = _hasher.VerifyHashedPassword(
            null!,
            passwordHash.Value,
            plainPassword);

        return result == PasswordVerificationResult.Success;
    }
}
