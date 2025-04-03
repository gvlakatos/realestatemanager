using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Data;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models.Reports;
using RealEstate.Core.Requests.Reports;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Handlers;

public class ReportHandler(AppDbContext context) : IReportHandler
{
    public async Task<Response<List<ActiveOwner>?>> GetActiveOwnersReportAsync()
    {
        try
        {
            var data = await context.ActiveOwners.AsNoTracking().ToListAsync();

            return new Response<List<ActiveOwner>?>(data);
        }
        catch
        {
            return new Response<List<ActiveOwner>?>(null, 500, "Não foi possível obter a lista de proprietários ativos");
        }
    }

    public async Task<Response<List<ActiveTenant>?>> GetActiveTenantsReportAsync()
    {
        try
        {
            var data = await context.ActiveTenants.AsNoTracking().ToListAsync();

            return new Response<List<ActiveTenant>?>(data);
        }
        catch
        {
            return new Response<List<ActiveTenant>?>(null, 500, "Não foi possível obter a lista de inquilinos ativos");
        }
    }

    public async Task<Response<List<PropertiesByStatus>?>> GetPropertiesByStatusReportAsync()
    {
        try
        {
            var data = await context.PropertiesByStatus.AsNoTracking().ToListAsync();

            return new Response<List<PropertiesByStatus>?>(data);
        }
        catch
        {
            return new Response<List<PropertiesByStatus>?>(null, 500, "Não foi possível obter a lista de propriedades");
        }
    }
}