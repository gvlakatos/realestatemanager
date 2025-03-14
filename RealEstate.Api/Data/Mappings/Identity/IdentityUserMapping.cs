using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Api.Models;

namespace RealEstate.Api.Data.Mappings.Identity;

public class IdentityUserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("IdentityUsers");
        builder.HasKey(iu => iu.Id);
        
        builder.HasIndex(ui => ui.NormalizedUserName).IsUnique();
        builder.HasIndex(ui => ui.NormalizedEmail).IsUnique();
        
        builder.Property(ui => ui.Email).HasMaxLength(180);
        builder.Property(ui => ui.NormalizedEmail).HasMaxLength(180);
        builder.Property(ui => ui.UserName).HasMaxLength(180);
        builder.Property(ui => ui.NormalizedUserName).HasMaxLength(180);
        builder.Property(ui => ui.PhoneNumber).HasMaxLength(20);
        builder.Property(ui => ui.ConcurrencyStamp).IsConcurrencyToken();
        
        builder.HasMany<IdentityUserClaim<long>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<long>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
        builder.HasMany<IdentityUserToken<long>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<long>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
    }
}