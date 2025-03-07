using RealEstate.Core.Models;
using RealEstate.Core.Requests.Tenants;
using RealEstate.Core.Responses;

namespace RealEstate.Core.Handlers;

public interface ITenantHandler
{
    Task<Response<Tenant?>> CreateAsync(CreateTenantRequest request);
    Task<Response<Tenant?>> UpdateAsync(UpdateTenantRequest request);
    Task<Response<Tenant?>> DeleteAsync(DeleteTenantRequest request);
    Task<Response<Tenant?>> GetByIdAsync(GetTenantByIdRequest request);
    Task<PagedResponse<List<Tenant>>> GetAllAsync(GetAllTenantsRequest request);
}