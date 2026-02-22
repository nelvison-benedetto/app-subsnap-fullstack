using Microsoft.EntityFrameworkCore;
using SubSnap.Core.Domain.Aggregates;
using SubSnap.Core.Domain.ValueObjects;
using SubSnap.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Infrastructure.DataLoaders;

public class UserAggregateLoader
{
    private readonly ApplicationDbContext _context;
    public UserAggregateLoader(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<UserSubscriptionsAggregate?> LoadWithSubscriptions(
        UserId userId,
        CancellationToken ct = default)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (user is null)
            return null;
        var subscriptions = await _context.Subscriptions
            .Where(s => EF.Property<Guid>(s, "UserId") == userId.Value)  //uso della SHADOW FK!!
            .AsNoTracking()
            .ToListAsync(ct);
        return new UserSubscriptionsAggregate(user, subscriptions);
    }
}
