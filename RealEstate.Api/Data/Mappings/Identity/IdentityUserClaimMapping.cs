using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Api.Data.Mappings.Identity;

public class IdentityUserClaimMapping : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
    {
        builder.ToTable("IdentityUserClaims");
        builder.HasKey(ic => ic.Id);
        builder.Property(ic => ic.ClaimType).IsRequired();
        builder.Property(ic => ic.ClaimValue).HasMaxLength(255);
    }
}