using SubSnap.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Abstractions.Identity;

public interface IAspNetPasswordHasher
{
    string Hash(string plainPassword);
    bool Verify(string plainPassword, PasswordHash passwordHash);
}
