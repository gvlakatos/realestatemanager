using RealEstate.Core.Models;

namespace RealEstate.Application.Repositories;

public interface IPropertyRepository
{
    Task<Property> GetByIdAsync(Guid id);
    Task<IEnumerable<Property>> GetAllAsync();
    Task CreateAsync(Property property);
    Task UpdateAsync(Property property);
    Task DeleteAsync(Property property);
    Task<List<Property>> GetAvailablePropertiesAsync();
}