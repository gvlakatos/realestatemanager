using RealEstate.Core.Models;
using RealEstate.Core.Requests.Properties;
using RealEstate.Core.Responses;

namespace RealEstate.Core.Handlers;

public interface IPropertyHandler
{
    Task<Response<Property?>> CreateAsync(CreatePropertyRequest request);
    Task<Response<Property?>> UpdateAsync(UpdatePropertyRequest request);
    Task<Response<Property?>> DeleteAsync(DeletePropertyRequest request);
    Task<Response<Property?>> GetByIdAsync(GetPropertyByIdRequest request);
    Task<PagedResponse<List<Property>>> GetAllAsync(GetAllPropertiesRequest request);
}