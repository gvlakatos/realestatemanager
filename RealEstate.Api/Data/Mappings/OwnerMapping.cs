using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Core.Models;

namespace RealEstate.Infrastructure.Data.Mappings;

public class OwnerMapping : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owners");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(o => o.CpfCnpj)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

        builder.Property(o => o.PhoneNumber)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

        builder.Property(o => o.Email)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.HasMany(o => o.Properties)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}