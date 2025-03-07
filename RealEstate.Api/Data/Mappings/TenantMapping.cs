using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Core.Models;

namespace RealEstate.Infrastructure.Data.Mappings;

public class TenantMapping : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.CpfCnpj)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.HasMany<LeaseContract>()
            .WithOne(lc => lc.Tenant)
            .HasForeignKey(lc => lc.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}