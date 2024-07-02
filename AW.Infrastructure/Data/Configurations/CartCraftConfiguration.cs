using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class CartCraftConfiguration : IEntityTypeConfiguration<CartCraft>
{
    public void Configure(EntityTypeBuilder<CartCraft> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__CartCraft__3214E117XD873AD6");

        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

            builder.HasOne(d => d.Cart).WithMany(p => p.CartCraft)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartCraft_Cart");

            builder.HasOne(d => d.Craft).WithMany(p => p.CartCraft)
                .HasForeignKey(d => d.CraftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartCraft_Craft");
    }
}