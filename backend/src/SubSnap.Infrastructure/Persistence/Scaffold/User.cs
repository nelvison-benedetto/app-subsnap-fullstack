using System;
using System.Collections.Generic;

namespace SubSnap.Infrastructure.Persistence.Scaffold;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<SharedLink> SharedLink { get; set; } = new List<SharedLink>();

    public virtual ICollection<Subscription> Subscription { get; set; } = new List<Subscription>();
}
