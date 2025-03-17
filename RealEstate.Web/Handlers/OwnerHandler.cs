using System.Net.Http.Json;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;

namespace RealEstate.Web.Handlers;

public class OwnerHandler(IHttpClientFactory httpClientFactory) : IOwnerHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Owner?>> CreateAsync(CreateOwnerRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/owners", request);
        return await result.Content.ReadFromJsonAsync<Response<Owner>>()
            ?? new Response<Owner?>(null, 400, "Error while creating owner");
    }

    public async Task<Response<Owner?>> UpdateAsync(UpdateOwnerRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/owners/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Owner>>()
               ?? new Response<Owner?>(null, 400, "Error while updating owner");
    }

    public async Task<Response<Owner?>> DeleteAsync(DeleteOwnerRequest request)
    {
        var result = await _client.DeleteAsync($"v1/owners/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Owner>>()
               ?? new Response<Owner?>(null, 400, "Error while deleting owner");
    }

    public async Task<Response<Owner?>> GetByIdAsync(GetOwnerByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Owner?>>($"v1/owners/{request.Id}")
        ?? new Response<Owner?>(null, 400, "Error while getting owner");

    public async Task<PagedResponse<List<Owner>>> GetAllAsync(GetAllOwnersRequest request) 
        => await _client.GetFromJsonAsync<PagedResponse<List<Owner>>>("v1/owners")
        ?? new PagedResponse<List<Owner>>(null, 400, "Error while getting owners");
}