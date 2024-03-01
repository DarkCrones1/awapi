using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class CraftmanDocumentConfiguration : IEntityTypeConfiguration<CraftmanDocument>
{
    public void Configure(EntityTypeBuilder<CraftmanDocument> builder)
    {
        builder.Property(e => e.ExpirationDate).HasColumnType("datetime");
        builder.Property(e => e.Value).HasMaxLength(50);

        builder.HasOne(d => d.Document).WithMany(p => p.CraftmanDocument)
            .HasForeignKey(d => d.DocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_EmployeeDocument_AWDocument");

        builder.HasOne(d => d.Craftman).WithMany(p => p.CraftmanDocument)
            .HasForeignKey(d => d.CraftmanId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CraftmanDocument_Craftman");
    }
}