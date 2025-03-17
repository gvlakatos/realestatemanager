using System.Net.Http.Json;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Tenants;
using RealEstate.Core.Responses;

namespace RealEstate.Web.Handlers;

public class TenantHandler(IHttpClientFactory httpClientFactory) : ITenantHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Tenant?>> CreateAsync(CreateTenantRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/tenants", request);
        return await result.Content.ReadFromJsonAsync<Response<Tenant>>()
               ?? new Response<Tenant?>(null, 400, "Error while creating tenant");
    }

    public async Task<Response<Tenant?>> UpdateAsync(UpdateTenantRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/tenants/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Tenant>>()
               ?? new Response<Tenant?>(null, 400, "Error while updating tenant");
    }

    public async Task<Response<Tenant?>> DeleteAsync(DeleteTenantRequest request)
    {
        var result = await _client.DeleteAsync($"v1/tenants/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Tenant>>()
               ?? new Response<Tenant?>(null, 400, "Error while deleting tenant");
    }

    public async Task<Response<Tenant?>> GetByIdAsync(GetTenantByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Tenant?>>($"v1/tenants/{request.Id}")
           ?? new Response<Tenant?>(null, 400, "Error while getting tenant");

    public async Task<PagedResponse<List<Tenant>>> GetAllAsync(GetAllTenantsRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Tenant>>>("v1/tenants")
           ?? new PagedResponse<List<Tenant>>(null, 400, "Error while getting tenants");
}