using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Models;
using RealEstate.Core.Models;
using RealEstate.Core.Models.Reports;

namespace RealEstate.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, IdentityRole<long>, long, IdentityUserClaim<long>, IdentityUserRole<long>, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>(options)
{
    public DbSet<Owner> Owners { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Property> Properties { get; set; } = null!;
    public DbSet<LeaseContract> LeaseContracts { get; set; } = null!;
    
    public DbSet<ActiveOwner> ActiveOwners { get; set; } = null!;
    public DbSet<ActiveTenant> ActiveTenants { get; set; } = null!;
    public DbSet<PropertiesByStatus> PropertiesByStatus { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Entity<ActiveOwner>().HasNoKey().ToView("vwGetActiveOwners");
        modelBuilder.Entity<ActiveTenant>().HasNoKey().ToView("vwGetActiveTenants");
        modelBuilder.Entity<PropertiesByStatus>().HasNoKey().ToView("vwGetPropertiesByStatus");
    }
}