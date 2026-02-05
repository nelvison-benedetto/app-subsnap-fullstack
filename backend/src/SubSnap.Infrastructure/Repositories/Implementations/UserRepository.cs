using Microsoft.EntityFrameworkCore;
using SubSnap.Core.Contracts.Repositories;
using SubSnap.Core.Domain.Aggregates;
using SubSnap.Core.Domain.Entities;
using SubSnap.Core.Domain.ValueObjects;
using SubSnap.Infrastructure.Mapping;
using SubSnap.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Infrastructure.Repositories.Implementations;

//repository = composizione query
//here NO savechangesasync, because it's done in unit of work

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Core.Domain.Entities.User?> GetByIdAsync(UserId id)  //Task<User?> because it can return null, return type is domain User
    {
        var entity = await _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == id.Value);
        //ora entity è di type 'Infrastructure.Persistence.Scaffold.User?'
        return entity is null ? null : UserMapper.ToDomain(entity);  //mapping entity found to domain!!
    }

    public async Task<User?> GetByEmailAsync(Email email)
    {
        var entity = await _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email.Value);
        return entity is null ? null : UserMapper.ToDomain(entity);
    }

    //aggregates
    public async Task<UserAggregate?> GetAggregateAsync(UserId id)
    {
        var entity = await _context.User
            .Include(u => u.Subscription)
            .Include(u => u.SharedLink)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == id.Value);

        return entity is null ? null : UserAggregateMapper.ToDomain(entity);
    }

    //commands
    public async Task AddAsync(User user)
    {
        _context.User.Add(UserMapper.ToEntity(user));
    }
}
