using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Repositories;
using RealEstate.Core.Entities;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class OwnerRepository(AppDbContext context) : IOwnerRepository
{
    public async Task<Owner> CreateAsync(Owner entity)
    {
        await context.Owners.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Owner> UpdateAsync(Owner entity)
    {
        context.Owners.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public Task<Owner> GetByIdAsync(Guid id)
    {
        return context.Owners.FirstOrDefaultAsync(o => o.Id == id);
    }
}