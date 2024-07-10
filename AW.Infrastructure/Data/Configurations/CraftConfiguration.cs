using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class CraftConfiguration : IEntityTypeConfiguration<Craft>
{
    public void Configure(EntityTypeBuilder<Craft> builder)
    {
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.Description).HasMaxLength(150);
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.PublicationDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.History).HasMaxLength(10000);

        builder.HasOne(d => d.Craftman).WithMany(p => p.Craft)
            .HasForeignKey(d => d.CraftmanId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Craft_Craftman");

        builder.HasOne(d => d.Culture).WithMany(p => p.Craft)
            .HasForeignKey(d => d.CultureId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Craft_Culture");

        builder.HasMany(d => d.Category).WithMany(p => p.Craft)
            .UsingEntity<Dictionary<string, object>>(
                "CraftCategory",
                r => r.HasOne<Category>().WithMany()
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CraftCategory_Category"),
                l => l.HasOne<Craft>().WithMany()
                    .HasForeignKey("CraftId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CraftCategory_Craft"),
                j =>
                {
                    j.HasKey("CraftId", "CategoryId");
                });

        builder.HasMany(d => d.TechniqueType).WithMany(p => p.Craft)
            .UsingEntity<Dictionary<string, object>>(
                "CraftTechniqueType",
                r => r.HasOne<TechniqueType>().WithMany()
                    .HasForeignKey("TechniqueTypeId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CraftTechniqueType_TechniqueType"),
                l => l.HasOne<Craft>().WithMany()
                    .HasForeignKey("CraftId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CraftTechniqueType_Craft"),
                j =>
                {
                    j.HasKey("CraftId", "TechniqueTypeId");
                });
    }
}