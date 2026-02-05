using SubSnap.Core.Domain.Aggregates;
using SubSnap.Core.Domain.Entities;
using SubSnap.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Contracts.Repositories;

public interface IUserRepository
{
    //entity singola
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByEmailAsync(Email email);

    //aggregates
    Task<UserAggregate?> GetAggregateAsync(UserId id);

    //commands
    Task AddAsync(User user);
}
