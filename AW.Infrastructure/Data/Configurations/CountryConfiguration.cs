using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(e => e.Code).HasMaxLength(10);
        builder.Property(e => e.CreatedBy).HasMaxLength(20);
        builder.Property(e => e.CreatedDate).HasColumnType("datetime");
        builder.Property(e => e.LastModifiedBy).HasMaxLength(20);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.Name).HasMaxLength(50);
    }
}