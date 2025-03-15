using System.Net.Http.Json;
using System.Text;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Identity;
using RealEstate.Core.Responses;

namespace RealEstate.Web.Handlers;

public class IdentityHandler(IHttpClientFactory httpClientFactory) : IIdentityHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/register", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Register successful", 200, "Register successful!")
            : new Response<string>(null, 400, "Register failed");
    }

    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Login successful", 200, "Login successful!")
            : new Response<string>(null, 400, "Login failed");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
    }
}