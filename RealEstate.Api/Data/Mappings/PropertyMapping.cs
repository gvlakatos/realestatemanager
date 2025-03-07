using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Core.Models;

namespace RealEstate.Infrastructure.Data.Mappings;

public class PropertyMapping : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.PropertyType)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(p => p.TransactionType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(p => p.PropertyStatus)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Description)
            .HasColumnType("NVARCHAR");

        builder.HasOne(p => p.Owner)
            .WithMany(o => o.Properties)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.CurrentContract)
            .WithOne(lc => lc.Property)
            .HasForeignKey<LeaseContract>(lc => lc.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}