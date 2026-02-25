using SubSnap.Application.Ports.Persistence;
using SubSnap.Infrastructure.Persistence.Context;

namespace SubSnap.Infrastructure.Persistence.UnitOfWork;

public sealed class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EFUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    { 
        await _context.SaveChangesAsync(ct);
    }
}
