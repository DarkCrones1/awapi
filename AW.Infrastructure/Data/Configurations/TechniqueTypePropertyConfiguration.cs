using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class TechniqueTypePropertyConfiguration : IEntityTypeConfiguration<TechniqueTypeProperty>
{
    public void Configure(EntityTypeBuilder<TechniqueTypeProperty> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__TechniqueType__324I3T076D873AD6");

        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.Property).WithMany(p => p.TechniqueTypePropertyc)
            .HasForeignKey(d => d.PropertyId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TechniqueTypeProperty_Property");

        builder.HasOne(d => d.TechniqueType).WithMany(p => p.TechniqueTypeProperty)
            .HasForeignKey(d => d.TechniqueTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TechniqueTypeProperty_TechniqueType");
    }
}