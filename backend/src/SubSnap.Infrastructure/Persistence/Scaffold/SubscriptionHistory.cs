using System;
using System.Collections.Generic;

namespace SubSnap.Infrastructure.Persistence.Scaffold;

public partial class SubscriptionHistory
{
    public int SubscriptionHistoryId { get; set; }

    public int SubscriptionId { get; set; }

    public string Action { get; set; } = null!;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;
}
