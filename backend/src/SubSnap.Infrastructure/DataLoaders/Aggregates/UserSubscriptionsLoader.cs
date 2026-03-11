using Microsoft.EntityFrameworkCore;
using SubSnap.Core.Domain.Entities;
using SubSnap.Core.Domain.ValueObjects;
using SubSnap.Infrastructure.Persistence.Context;

namespace SubSnap.Infrastructure.DataLoaders.Aggregates;

// usa AutoMapper nel .API per e.g.UserAggregate → UserDashboardDTO e passare questo al client.
public sealed class UserSubscriptionsLoader : IUserSubscriptionsLoader
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory; //x .WhenAll() cioe query in parallelo

    public UserSubscriptionsLoader(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<SubscriptionSubscriptionHistories> LoadWithSubscriptionHistories(SubscriptionId subscriptionId, CancellationToken ct)
    {
        await using var context = await _factory.CreateDbContextAsync(ct);

        var subscription = await context.Subscriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == subscriptionId, ct);

        if (subscription == null) return null;

        //parallel loads dei children targets
        var subscriptionHistoriesTask = context.Set<SubscriptionHistory>()
            .AsNoTracking()
            .Where(sh => EF.Property<Guid>(sh, "SubscriptionId") == subscriptionId.Value)
            .ToListAsync(ct);

        await Task.WhenAll(subscriptionHistoriesTask);

        return new SubscriptionSubscriptionHistories(
            subscription,
            subscriptionHistoriesTask.Result
        );
    }

}
