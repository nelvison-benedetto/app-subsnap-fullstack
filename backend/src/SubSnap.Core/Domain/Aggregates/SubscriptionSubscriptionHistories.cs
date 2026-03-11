using SubSnap.Core.Domain.Entities;

namespace SubSnap.Core.Domain.Aggregates;

public class SubscriptionSubscriptionHistories
{
    public Subscription Subscription { get; }
    public IReadOnlyCollection<SubscriptionHistory> SubscriptionHistories { get; }

    public SubscriptionSubscriptionHistories(
        Subscription subscription,
        IReadOnlyCollection<SubscriptionHistory> subscriptionHistories
    )
    {
        Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
        SubscriptionHistories = subscriptionHistories?.ToList().AsReadOnly()
            ?? throw new ArgumentNullException(nameof(subscriptionHistories));
    }
}
