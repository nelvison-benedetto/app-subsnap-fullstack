using SubSnap.Core.Domain.Entities;

namespace SubSnap.Infrastructure.DataLoaders.Projections.Views;

/*
non va nel Domain, xk è un read models delle query!! non è un business aggregate! non rappresenta uno STATO reale di dominio!!
*/
public sealed class UserSubscriptionsProjectionView
{
    public User User { get; }
    public IReadOnlyCollection<Subscription> Subscriptions { get; }

    public UserSubscriptionsProjectionView(
        User user,
        IReadOnlyCollection<Subscription> subscriptions
    )
    {
        User = user ?? throw new ArgumentNullException(nameof(user));

        Subscriptions = subscriptions?.ToList().AsReadOnly()
            ?? throw new ArgumentNullException(nameof(subscriptions));
    }
}
