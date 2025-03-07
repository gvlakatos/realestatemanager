using RealEstate.Core.Models;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;

namespace RealEstate.Core.Handlers;

public interface IOwnerHandler
{
    Task<Response<Owner?>> CreateAsync(CreateOwnerRequest request);
    Task<Response<Owner?>> UpdateAsync(UpdateOwnerRequest request);
    Task<Response<Owner?>> DeleteAsync(DeleteOwnerRequest request);
    Task<Response<Owner?>> GetByIdAsync(GetOwnerByIdRequest request);
    Task<PagedResponse<List<Owner>>> GetAllAsync(GetAllOwnersRequest request);
}