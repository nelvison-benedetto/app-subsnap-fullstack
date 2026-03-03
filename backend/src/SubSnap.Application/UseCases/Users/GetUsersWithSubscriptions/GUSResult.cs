using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Application.UseCases.Users.GetUsersWithSubscriptions;

public sealed record GUSResult(
    Guid UserId,
    string Email,
    int SubscriptionCount
);
