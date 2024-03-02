using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class CraftPropertyConfiguration : IEntityTypeConfiguration<CraftProperty>
{
    public void Configure(EntityTypeBuilder<CraftProperty> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Craft__3214EC076D873AD6");

        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.Property).WithMany(p => p.CraftProperty)
            .HasForeignKey(d => d.PropertyId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CraftProperty_Property");

        builder.HasOne(d => d.Craft).WithMany(p => p.CraftProperty)
            .HasForeignKey(d => d.CraftId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CraftProperty_Craft");
    }
}