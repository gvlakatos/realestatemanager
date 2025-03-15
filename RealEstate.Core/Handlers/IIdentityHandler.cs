using RealEstate.Core.Requests.Identity;
using RealEstate.Core.Responses;

namespace RealEstate.Core.Handlers;

public interface IIdentityHandler
{
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}