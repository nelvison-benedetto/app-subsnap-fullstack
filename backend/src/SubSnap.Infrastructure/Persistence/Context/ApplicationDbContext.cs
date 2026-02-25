using Microsoft.EntityFrameworkCore;
using SubSnap.Core.Domain.Entities;

namespace SubSnap.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<User> Users => Set<User>();  //solo la root dell'aggregate deve essere esposto al dbset.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
