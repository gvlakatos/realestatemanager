using System.Net.Http.Json;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Properties;
using RealEstate.Core.Responses;

namespace RealEstate.Web.Handlers;

public class PropertyHandler(IHttpClientFactory httpClientFactory) : IPropertyHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Property?>> CreateAsync(CreatePropertyRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/properties", request);
        return await result.Content.ReadFromJsonAsync<Response<Property>>()
               ?? new Response<Property?>(null, 400, "Error while creating property");
    }

    public async Task<Response<Property?>> UpdateAsync(UpdatePropertyRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/properties/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Property>>()
               ?? new Response<Property?>(null, 400, "Error while updating property");
    }

    public async Task<Response<Property?>> DeleteAsync(DeletePropertyRequest request)
    {
        var result = await _client.DeleteAsync($"v1/properties/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Property>>()
               ?? new Response<Property?>(null, 400, "Error while deleting property");
    }

    public async Task<Response<Property?>> GetByIdAsync(GetPropertyByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Property?>>($"v1/properties/{request.Id}")
           ?? new Response<Property?>(null, 400, "Error while getting property");

    public async Task<PagedResponse<List<Property>>> GetAllAsync(GetAllPropertiesRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Property>>>("v1/properties")
           ?? new PagedResponse<List<Property>>(null, 400, "Error while getting properties");
}