using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AW.Domain.Entities;
using System.Globalization;

namespace AW.Infrastructure.Data.Configurations;

public class UserDataInfoConfiguration : IEntityTypeConfiguration<UserDataInfo>
{
    public void Configure(EntityTypeBuilder<UserDataInfo> builder)
    {
        builder.Ignore(x => x.FullName);
        builder.HasKey(e => e.Id).HasName("PK__UserDataInfo__8574EC235D7D5044");


        builder.Property(e => e.CellPhone)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.FirstName)
            .HasMaxLength(200)
            .IsUnicode(false);
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.LastName)
            .HasMaxLength(200)
            .IsUnicode(false);
        builder.Property(e => e.MiddleName)
            .HasMaxLength(150)
            .IsUnicode(false);
        builder.Property(e => e.Phone)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.ProfilePictureUrl).HasMaxLength(250);
    }
}