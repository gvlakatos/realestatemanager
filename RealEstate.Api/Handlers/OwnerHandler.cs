using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Models;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;
using RealEstate.Api.Data;

namespace RealEstate.Api.Handlers;

public class OwnerHandler(AppDbContext context) : IOwnerHandler
{
    public async Task<Response<Owner?>> CreateAsync(CreateOwnerRequest request)
    {
        try
        {
            var owner = new Owner
            {
                Name = request.Name,
                CpfCnpj = request.CpfCnpj,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UpdatedAt = DateTime.UtcNow
            };

            await context.Owners.AddAsync(owner);
            await context.SaveChangesAsync();

            return new Response<Owner?>(owner, 201, "Owner created successfully!");
        }
        catch
        {
            return new Response<Owner?>(null, 500, "Error while creating a new owner");
        }
    }

    public async Task<Response<Owner?>> UpdateAsync(UpdateOwnerRequest request)
    {
        try
        {
            var owner = await context.Owners.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (owner is null)
                return new Response<Owner?>(null, 404, "Owner not found");

            owner.Name = request.Name;
            owner.CpfCnpj = request.CpfCnpj;
            owner.PhoneNumber = request.PhoneNumber;
            owner.Email = request.Email;
            owner.UpdatedAt = DateTime.UtcNow;
            owner.IsActive = request.IsActive;

            context.Owners.Update(owner);
            await context.SaveChangesAsync();

            return new Response<Owner?>(owner, message: "Owner updated successfully!");
        }
        catch
        {
            return new Response<Owner?>(null, 500, "Error while updating a owner");
        }
    }

    public async Task<Response<Owner?>> DeleteAsync(DeleteOwnerRequest request)
    {
        try
        {
            var owner = await context.Owners.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (owner is null)
                return new Response<Owner?>(null, 404, "Owner not found");

            context.Owners.Remove(owner);
            await context.SaveChangesAsync();

            return new Response<Owner?>(owner, message: "Owner removed successfully!");
        }
        catch
        {
            return new Response<Owner?>(null, 500, "Error while removing a owner");
        }
    }

    public async Task<Response<Owner?>> GetByIdAsync(GetOwnerByIdRequest request)
    {
        try
        {
            var owner = await context.Owners.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

            return owner is null
                ? new Response<Owner?>(null, 400, "Owner not found")
                : new Response<Owner?>(owner);
        }
        catch
        {
            return new Response<Owner?>(null, 500, "Error while retriving a owner");
        }
    }

    public async Task<PagedResponse<List<Owner>>> GetAllAsync(GetAllOwnersRequest request)
    {
        try
        {
            var query = context.Owners.AsNoTracking().OrderBy(x => x.Name);
            var owners = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Owner>>(owners, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Owner>>(null, 500, "Error while retriving owners list");
        }
    }
}