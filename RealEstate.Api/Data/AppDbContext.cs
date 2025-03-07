using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Models;

namespace RealEstate.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Owner> Owners { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Property> Properties { get; set; } = null!;
    public DbSet<LeaseContract> LeaseContracts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}