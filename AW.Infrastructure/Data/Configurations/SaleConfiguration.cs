using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.Property(e => e.Amount).HasColumnType("decimal(10,6)");
        builder.HasOne(d => d.Craftman).WithMany(p => p.Sale)
            .HasForeignKey(d => d.CraftmanId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Sale_Craftman");
    }
}