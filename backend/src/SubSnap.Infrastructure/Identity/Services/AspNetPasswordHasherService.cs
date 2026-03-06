using SubSnap.Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using SubSnap.Application.Ports.Auth;

namespace SubSnap.Infrastructure.Identity.Services;

public class AspNetPasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<object> _hasher = new();  //here PasswordHasher è di plugin Microsoft.AspNetCore.Identity

    public PasswordHash Hash(string plainPassword)
    {
        var hash = _hasher.HashPassword(null!, plainPassword);  //firma reale è TUuser user, string password. null! significa 'so che sto passando null, non generare warning giallo'.
        return new PasswordHash(hash);  //validation
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
