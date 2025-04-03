using RealEstate.Core.Models.Reports;
using RealEstate.Core.Requests.Reports;
using RealEstate.Core.Responses;

namespace RealEstate.Core.Handlers;

public interface IReportHandler
{
    Task<Response<List<ActiveOwner>?>> GetActiveOwnersReportAsync();
    Task<Response<List<ActiveTenant>?>> GetActiveTenantsReportAsync();
    Task<Response<List<PropertiesByStatus>?>> GetPropertiesByStatusReportAsync();
}