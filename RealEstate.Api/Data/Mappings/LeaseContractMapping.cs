using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Core.Models;

namespace RealEstate.Infrastructure.Data.Mappings;

public class LeaseContractMapping : IEntityTypeConfiguration<LeaseContract>
{
    public void Configure(EntityTypeBuilder<LeaseContract> builder)
    {
        builder.ToTable("LeaseContracts");

        builder.HasKey(lc => lc.Id);

        builder.Property(lc => lc.StartDate)
            .IsRequired();
        
        builder.Property(lc => lc.EndDate)
            .IsRequired();

        builder.Property(lc => lc.MonthlyAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(lc => lc.PaymentMethod)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(lc => lc.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(lc => lc.Property)
            .WithOne(p => p.CurrentContract)
            .HasForeignKey<LeaseContract>(lc => lc.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(lc => lc.Tenant)
            .WithMany(t => t.LeaseContracts)
            .HasForeignKey(lc => lc.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}