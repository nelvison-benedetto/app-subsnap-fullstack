using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubSnap.Core.Domain.Entities;
using SubSnap.Core.Domain.ValueObjects;

namespace SubSnap.Infrastructure.Persistence.Configurations;

public sealed class UserMediaConfiguration
    : IEntityTypeConfiguration<UserMedia>
{
    public void Configure(EntityTypeBuilder<UserMedia> builder)
    {
        builder.ToTable("usermedia");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new UserMediaId(value))
            .HasColumnType("uuid")
            .ValueGeneratedNever(); //!!, dice a EF di non aspettarsi che il db generei l'id(xk lo genero io nel Domain)

        builder.Property<Guid>("UserId")  //SHADOW FK
            .HasColumnName("userid")
            .IsRequired();  //sempre required!!

        builder.Property(x => x.ObjectKey)
            .HasColumnName("objectkey")
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasColumnName("contenttype")
            .HasMaxLength(100);

        builder.Property(x => x.Size)
            .HasColumnName("size");

        builder.Property(x => x.UploatedAt)
            .HasColumnName("uploadedat")
            .HasColumnType("timestamptz")  //non è (3), magari da cambiare sul db.
            .IsRequired();


        //x relazione User->Subscription ma senza navigation property(che è un problema xk non è loosing)!!
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);

    }
}