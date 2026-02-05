using SubSnap.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.Aggregates;

//aggregate esplicito, caricato solo quando serve!
public class UserSubscriptionsAggregate
{
    public User User { get; }
    public IReadOnlyCollection<Subscription> Subscriptions { get; }

    public UserSubscriptionsAggregate(
        User user,
        IReadOnlyCollection<Subscription> subscriptions)
    {
        User = user;
        Subscriptions = subscriptions;
    }
}
