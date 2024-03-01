using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class CustomerDocumentConfiguration : IEntityTypeConfiguration<CustomerDocument>
{
    public void Configure(EntityTypeBuilder<CustomerDocument> builder)
    {
        builder.Property(e => e.ExpirationDate).HasColumnType("datetime");
        builder.Property(e => e.Value).HasMaxLength(50);

        builder.HasOne(d => d.Customer).WithMany(p => p.CustomerDocument)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerDocument_Customer");

        builder.HasOne(d => d.Document).WithMany(p => p.CustomerDocument)
            .HasForeignKey(d => d.DocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerDocument_AWDocument");
    }
}