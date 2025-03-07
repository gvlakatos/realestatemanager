using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Data;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Tenants;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Handlers;

public class TenantHandler(AppDbContext context) : ITenantHandler
{
    public async Task<Response<Tenant?>> CreateAsync(CreateTenantRequest request)
    {
        try
        {
            var tenant = new Tenant
            {
                Name = request.Name,
                CpfCnpj = request.CpfCnpj,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UpdatedAt = DateTime.UtcNow
            };

            await context.Tenants.AddAsync(tenant);
            await context.SaveChangesAsync();

            return new Response<Tenant?>(tenant, 201, "Tenant created successfully!");
        }
        catch
        {
            return new Response<Tenant?>(null, 500, "Error while creating a new tenant");
        }
    }

    public async Task<Response<Tenant?>> UpdateAsync(UpdateTenantRequest request)
    {
        try
        {
            var tenant = await context.Tenants.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (tenant is null)
                return new Response<Tenant?>(null, 404, "Tenant not found");

            tenant.Name = request.Name;
            tenant.CpfCnpj = request.CpfCnpj;
            tenant.PhoneNumber = request.PhoneNumber;
            tenant.Email = request.Email;
            tenant.UpdatedAt = DateTime.UtcNow;

            context.Tenants.Update(tenant);
            await context.SaveChangesAsync();

            return new Response<Tenant?>(tenant, message: "Tenant updated successfully!");
        }
        catch
        {
            return new Response<Tenant?>(null, 500, "Error while updating a tenant");
        }
    }

    public async Task<Response<Tenant?>> DeleteAsync(DeleteTenantRequest request)
    {
        try
        {
            var tenant = await context.Tenants.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (tenant is null)
                return new Response<Tenant?>(null, 404, "Tenant not found");

            context.Tenants.Remove(tenant);
            await context.SaveChangesAsync();

            return new Response<Tenant?>(tenant, message: "Tenant removed successfully!");
        }
        catch
        {
            return new Response<Tenant?>(null, 500, "Error while removing a tenant");
        }
    }

    public async Task<Response<Tenant?>> GetByIdAsync(GetTenantByIdRequest request)
    {
        try
        {
            var tenant = await context.Tenants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

            return tenant is null
                ? new Response<Tenant?>(null, 400, "Tenant not found")
                : new Response<Tenant?>(tenant);
        }
        catch
        {
            return new Response<Tenant?>(null, 500, "Error while retriving a tenant");
        }
    }

    public async Task<PagedResponse<List<Tenant>>> GetAllAsync(GetAllTenantsRequest request)
    {
        try
        {
            var query = context.Tenants.AsNoTracking().OrderBy(x => x.Name);
            var tenants = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Tenant>>(tenants, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Tenant>>(null, 500, "Error while retriving tenants list");
        }
    }
}