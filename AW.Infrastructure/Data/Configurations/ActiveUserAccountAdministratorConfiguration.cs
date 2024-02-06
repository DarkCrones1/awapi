using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data.Configurations;

public class ActiveUserAccountAdministratorConfiguration : IEntityTypeConfiguration<ActiveUserAccountAdministrator>
{
    public void Configure(EntityTypeBuilder<ActiveUserAccountAdministrator> builder)
    {
        builder
                .HasNoKey()
                .ToView("VW_ActiveUserAccountAdministrator");

        builder.Property(e => e.CellPhone).HasMaxLength(20);
        builder.Property(e => e.Email).HasMaxLength(150);
        builder.Property(e => e.FirstName).HasMaxLength(200);
        builder.Property(e => e.LastName).HasMaxLength(200);
        builder.Property(e => e.MiddleName).HasMaxLength(150);
        builder.Property(e => e.Phone).HasMaxLength(20);
        builder.Property(e => e.UserName).HasMaxLength(150);

        // Configure id don't delete
        builder.Property(e => e.Id).HasColumnName("UserAccountId");
    }
}