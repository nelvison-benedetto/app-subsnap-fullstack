using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }

    private RefreshToken() { }

    internal RefreshToken(string token, DateTime expiresAt)
    {
        Id = Guid.NewGuid();
        Token = token;
        ExpiresAt = expiresAt;
    }

    public void Revoke() => IsRevoked = true;
}
