using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Api.Data.Mappings.Identity;

public class IdentityUserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
    {
        builder.ToTable("IdentityUserTokens");
        builder.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        builder.Property(ut => ut.LoginProvider).HasMaxLength(120);
        builder.Property(ut => ut.Name).HasMaxLength(180);
    }
}