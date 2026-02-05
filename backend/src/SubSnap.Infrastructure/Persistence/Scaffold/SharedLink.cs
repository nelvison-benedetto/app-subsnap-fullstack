using System;
using System.Collections.Generic;

namespace SubSnap.Infrastructure.Persistence.Scaffold;

public partial class SharedLink
{
    public Guid SharedLinkId { get; set; }

    public int UserId { get; set; }

    public string Link { get; set; } = null!;

    public DateTime? ExpireAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int Views { get; set; }

    public virtual User User { get; set; } = null!;
}
