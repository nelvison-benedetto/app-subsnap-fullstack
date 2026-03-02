using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubSnap.Infrastructure.Persistence.Outbox;

namespace SubSnap.Infrastructure.Persistence.Configurations;

//see User.cs  transactionbehavior.cs efunitofwork.cs  outboxprocessor.cs outboxmessage.cs

public sealed class OutboxMessageConfiguration
    : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outboxmessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Type)
            .HasColumnName("type")
            .IsRequired();

        builder.Property(x => x.Payload)
            .HasColumnName("payload")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(x => x.OccurredOnUtc)
            .HasColumnName("occurredonutc");

        builder.Property(x => x.ProcessedOnUtc)
            .HasColumnName("processedonutc");
    }
}