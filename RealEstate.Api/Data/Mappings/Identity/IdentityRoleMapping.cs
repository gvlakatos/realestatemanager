using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Api.Data.Mappings.Identity;

public class IdentityRoleMapping : IEntityTypeConfiguration<IdentityRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<long>> builder)
    {
        builder.ToTable("IdentityRoles");
        
        builder.HasKey(ir => ir.Id);
        builder.HasIndex(ir => ir.NormalizedName).IsUnique();
        
        builder.Property(ir => ir.ConcurrencyStamp).IsConcurrencyToken();
        builder.Property(ir => ir.Name).HasMaxLength(256);
        builder.Property(ir => ir.NormalizedName).HasMaxLength(256);
    }
}