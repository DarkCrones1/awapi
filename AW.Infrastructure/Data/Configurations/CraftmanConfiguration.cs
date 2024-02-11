using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class CraftmanConfiguration : IEntityTypeConfiguration<Craftman>
{
    public void Configure(EntityTypeBuilder<Craftman> builder)
    {
        builder.Ignore(x => x.FullName);
        builder.HasKey(e => e.Id).HasName("PK__Craftman__3214EC235D7D5044");


        builder.Property(e => e.CellPhone)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.FirstName)
            .HasMaxLength(200)
            .IsUnicode(false);
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.LastName)
            .HasMaxLength(200)
            .IsUnicode(false);
        builder.Property(e => e.MiddleName)
            .HasMaxLength(150)
            .IsUnicode(false);
        builder.Property(e => e.Phone)
            .HasMaxLength(50)
            .IsUnicode(false);
    }
}