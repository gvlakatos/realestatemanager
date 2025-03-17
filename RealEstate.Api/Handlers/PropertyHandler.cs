using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Data;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Properties;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Handlers;

public class PropertyHandler(AppDbContext context) : IPropertyHandler
{
    public async Task<Response<Property?>> CreateAsync(CreatePropertyRequest request)
    {
        try
        {
            var property = new Property
            {
                Address = request.Address,
                PropertyType = request.PropertyType,
                TransactionType = request.TransactionType,
                Amount = request.Amount,
                PropertyStatus = request.PropertyStatus,
                Description = request.Description,
                OwnerId = request.OwnerId,
            };
            
            await context.Properties.AddAsync(property);
            await context.SaveChangesAsync();
            
            return new Response<Property?>(property, 201, "Property created successfully!");
        }
        catch
        {
            return new Response<Property?>(null, 500, "Error while creating a new property");
        }
    }

    public async Task<Response<Property?>> UpdateAsync(UpdatePropertyRequest request)
    {
        try
        {
            var property = await context.Properties.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (property is null)
                return new Response<Property?>(null, 404, "Property not found");

            property.Address = request.Address;
            property.PropertyType = request.PropertyType;
            property.TransactionType = request.TransactionType;
            property.Amount = request.Amount;
            property.PropertyStatus = request.PropertyStatus;
            property.Description = request.Description;
            property.OwnerId = request.OwnerId;
            property.UpdatedAt = DateTime.UtcNow;

            context.Properties.Update(property);
            await context.SaveChangesAsync();

            return new Response<Property?>(property, message: "Property updated successfully!");
        }
        catch
        {
            return new Response<Property?>(null, 500, "Error while updating a property");
        }
    }

    public async Task<Response<Property?>> DeleteAsync(DeletePropertyRequest request)
    {
        try
        {
            var property = await context.Properties.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (property is null)
                return new Response<Property?>(null, 404, "Property not found");

            context.Properties.Remove(property);
            await context.SaveChangesAsync();

            return new Response<Property?>(property, message: "Property removed successfully!");
        }
        catch
        {
            return new Response<Property?>(null, 500, "Error while removing a property");
        }
    }

    public async Task<Response<Property?>> GetByIdAsync(GetPropertyByIdRequest request)
    {
        try
        {
            var property = await context.Properties.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

            return property is null
                ? new Response<Property?>(null, 400, "Property not found")
                : new Response<Property?>(property);
        }
        catch
        {
            return new Response<Property?>(null, 500, "Error while retriving a property");
        }
    }

    public async Task<PagedResponse<List<Property>>> GetAllAsync(GetAllPropertiesRequest request)
    {
        try
        {
            var query = context.Properties.AsNoTracking();
            var properties = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Property>>(properties, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Property>>(null, 500, "Error while retriving properties list");
        }
    }
}