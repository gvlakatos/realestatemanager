using System.Net.Http.Json;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models.Reports;
using RealEstate.Core.Responses;

namespace RealEstate.Web.Handlers;

public class ReportHandler(IHttpClientFactory httpClientFactory) : IReportHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<List<ActiveOwner>?>> GetActiveOwnersReportAsync()
    {
        return await _client.GetFromJsonAsync<Response<List<ActiveOwner>?>>("v1/reports/getActiveOwners") ?? new Response<List<ActiveOwner>?>(null, 400, "Não foi possível recuperar dados");
    }

    public async Task<Response<List<ActiveTenant>?>> GetActiveTenantsReportAsync()
    {
        return await _client.GetFromJsonAsync<Response<List<ActiveTenant>?>>("v1/reports/getActiveTenants") ?? new Response<List<ActiveTenant>?>(null, 400, "Não foi possível recuperar dados");
    }

    public async Task<Response<List<PropertiesByStatus>?>> GetPropertiesByStatusReportAsync()
    {
        return await _client.GetFromJsonAsync<Response<List<PropertiesByStatus>?>>("v1/reports/getPropertiesByStatus") ?? new Response<List<PropertiesByStatus>>(null, 400, "Não foi possível recuperar dados");
    }
}