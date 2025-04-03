using RealEstate.Core.Models;
using RealEstate.Core.Requests.LeaseContracts;
using RealEstate.Core.Responses;

namespace RealEstate.Core.Handlers;

public interface ILeaseContractHandler
{
    Task<Response<LeaseContract?>> CreateAsync(CreateLeaseContractRequest request);
}