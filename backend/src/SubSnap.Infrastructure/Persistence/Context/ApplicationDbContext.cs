using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SubSnap.Infrastructure.Persistence.Scaffold;

namespace SubSnap.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SharedLink> SharedLink { get; set; }

    public virtual DbSet<Subscription> Subscription { get; set; }

    public virtual DbSet<SubscriptionHistory> SubscriptionHistory { get; set; }

    public virtual DbSet<User> User { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=legion5\\sqlexpress;Initial Catalog=AppSubSnapDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SharedLink>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_SharedLink_Update"));

            entity.HasIndex(e => e.UserId, "IX_SharedLink_UserId");

            entity.Property(e => e.SharedLinkId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ExpireAt).HasPrecision(3);
            entity.Property(e => e.Link).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.SharedLink)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SharedLink_User");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_Subscription_Update"));

            entity.HasIndex(e => e.UserId, "IX_Subscription_UserId");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BillingCycle)
                .HasMaxLength(50)
                .HasDefaultValue("Mensile");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.Subscription)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subscription_User");
        });

        modelBuilder.Entity<SubscriptionHistory>(entity =>
        {
            entity.HasIndex(e => e.SubscriptionId, "IX_SubscriptionHistory_SubscriptionId");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionHistory)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubscriptionHistory_Subscription");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_User_Update"));

            entity.HasIndex(e => e.Email, "UQ_User").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.LastLogin).HasPrecision(3);
            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
