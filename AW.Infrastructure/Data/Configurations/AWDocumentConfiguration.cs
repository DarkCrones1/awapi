using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class AWDocumentConfiguration : IEntityTypeConfiguration<AWDocument>
{
    public void Configure(EntityTypeBuilder<AWDocument> builder)
    {
        // Configuration Additional don't delete
        builder.ToTable("AWDocument");

        builder.HasKey(e => e.Id).HasName("PK__Document__3214EC07816C85DA");

        builder.Property(e => e.Description)
            .HasMaxLength(150)
            .IsUnicode(false);
        builder.Property(e => e.Extension).HasMaxLength(5);
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.UrlDocument)
            .HasMaxLength(250)
            .HasColumnName("URLDocument");
    }
}