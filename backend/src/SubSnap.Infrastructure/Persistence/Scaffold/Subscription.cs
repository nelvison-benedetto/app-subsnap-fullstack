using System;
using System.Collections.Generic;

namespace SubSnap.Infrastructure.Persistence.Scaffold;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Amount { get; set; }

    public string BillingCycle { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Category { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<SubscriptionHistory> SubscriptionHistory { get; set; } = new List<SubscriptionHistory>();

    public virtual User User { get; set; } = null!;
}
