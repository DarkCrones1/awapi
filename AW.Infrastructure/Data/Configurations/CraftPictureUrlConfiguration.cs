using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class CraftPictureUrlConfiguration : IEntityTypeConfiguration<CraftPictureUrl>
{
    public void Configure(EntityTypeBuilder<CraftPictureUrl> builder)
    {
        builder.Property(e => e.ImageUrl).HasMaxLength(200);

        builder.HasOne(d => d.Craft)
            .WithMany(p => p.CraftPictureUrl)
            .HasForeignKey(d => d.CraftId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CraftPictureUrl_Craft");
    }
}