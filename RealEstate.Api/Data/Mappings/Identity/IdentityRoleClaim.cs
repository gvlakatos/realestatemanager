using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Api.Data.Mappings.Identity;

public class IdentityRoleClaim : IEntityTypeConfiguration<IdentityRoleClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
    {
        builder.ToTable("IdentityRoleClaims");
        
        builder.HasKey(rc => rc.Id);
        
        builder.Property(rc => rc.ClaimType).HasMaxLength(255);
        builder.Property(rc => rc.ClaimValue).HasMaxLength(255);
    }
}