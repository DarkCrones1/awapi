using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.Password).HasMaxLength(100);
        builder.Property(e => e.UserName).HasMaxLength(150);
        builder.Property(e => e.Email)
            .HasMaxLength(150)
            .IsUnicode(false);

        builder.HasMany(d => d.Administrator).WithMany(p => p.UserAccount)
            .UsingEntity<Dictionary<string, object>>(
                "UserAccountAdministrator",
                r => r.HasOne<Administrator>().WithMany()
                    .HasForeignKey("AdministratorId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountAdministrator_Administrator"),
                l => l.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasForeignKey("FK_UserAccountAdministrator_UserAccount"),
                j =>
                {
                    j.HasKey("UserAccountId", "AdministratorId");
                });

        builder.HasMany(d => d.Customer).WithMany(p => p.UserAccount)
            .UsingEntity<Dictionary<string, object>>(
                "UserAccountCustomer",
                r => r.HasOne<Customer>().WithMany()
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountCustomer_Customer"),
                l => l.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_UserAccountCustomer_UserAccount"),
                j =>
                {
                    j.HasKey("UserAccountId", "CustomerId");
                });

        builder.HasMany(d => d.Craftman).WithMany(p => p.UserAccount)
            .UsingEntity<Dictionary<string, object>>(
                "UserAccountCraftman",
                r => r.HasOne<Craftman>().WithMany()
                    .HasForeignKey("CraftmanId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountCraftman_Craftman"),
                l => l.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_UserAccountCraftman_UserAccount"),
                j =>
                {
                    j.HasKey("UserAccountId", "CraftmanId");
                });

        builder.HasMany(d => d.Rol).WithMany(p => p.UserAccount)
            .UsingEntity<Dictionary<string, object>>(
                "UserRol",
                r => r.HasOne<Rol>().WithMany()
                    .HasForeignKey("RolId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRol_Rol"),
                l => l.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRol_UserAccount"),
                j =>
                {
                    j.HasKey("UserAccountId", "RolId");
                });
    }
}