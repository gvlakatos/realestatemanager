using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstate.Api.Data.Mappings.Identity;

public class IdentityUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
    {
        builder.ToTable("IdentityUserLogins");
        builder.HasKey(il => new { il.LoginProvider, il.ProviderKey });
        builder.Property(il => il.LoginProvider).HasMaxLength(128);
        builder.Property(il => il.ProviderKey).HasMaxLength(128);
        builder.Property(il => il.ProviderKey).HasMaxLength(255);
    }
}