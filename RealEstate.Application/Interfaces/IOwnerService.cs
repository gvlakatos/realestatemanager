using RealEstate.Core.Entities;
using RealEstate.Core.Requests.Owners;

namespace RealEstate.Application.Interfaces;

public interface IOwnerService
{
    Task<Owner> CreateAsync(CreateOwnerRequest request);
    Task<Owner> UpdateAsync(UpdateOwnerRequest request);
    Task<Owner> GetByIdAsync(GetOwnerByIdRequest request);
}