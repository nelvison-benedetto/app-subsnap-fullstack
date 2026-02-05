using SubSnap.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.Aggregates;

//questo è il 'VERO' User completo(w list Subscriptions e list SharedLinks). NON sempre caricato, solo quando serve
public sealed class UserAggregate
{
    public User User { get; }
    public IReadOnlyCollection<Subscription> Subscriptions { get; }
    public IReadOnlyCollection<SharedLink> SharedLinks { get; }

    public UserAggregate(
        User user,
        IEnumerable<Subscription> subscriptions,
        IEnumerable<SharedLink> sharedLinks)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Subscriptions = subscriptions?.ToList().AsReadOnly()
            ?? throw new ArgumentNullException(nameof(subscriptions));
        SharedLinks = sharedLinks?.ToList().AsReadOnly()
            ?? throw new ArgumentNullException(nameof(sharedLinks));
    }
}
