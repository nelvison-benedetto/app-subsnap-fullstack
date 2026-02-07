using SubSnap.Core.Abstractions.Identity;
using SubSnap.Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace SubSnap.Infrastructure.Identity.Services;

public class AspNetPasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<object> _hasher = new();  //here PasswordHasher è di plugin Microsoft.AspNetCore.Identity

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
        return result == PasswordVerificationResult.Success;  //here PasswordVerificationResult è di plugin Microsoft.AspNetCore.Identity
    }
}
