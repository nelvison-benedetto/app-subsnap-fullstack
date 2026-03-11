using SubSnap.Core.Domain.Aggregates;
using SubSnap.Core.Domain.ValueObjects;

namespace SubSnap.Application.Ports.DataLoadersorQueries;

public interface IUserAggregateLoader
{
    Task<UserFullAggregate?> LoadWithFull(UserId userId, CancellationToken ct = default);
    Task<UserSharedLinksAggregate> LoadWithSharedLinks(UserId userId, CancellationToken ct);


    //wrong!! 2 aggregate roots
    Task<UserSubscriptionsAggregate?> LoadWithSubscriptions(UserId userId, CancellationToken ct = default);

}
