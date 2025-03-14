using Microsoft.AspNetCore.Identity;
using RealEstate.Api.Commom.Api;
using RealEstate.Api.Models;

namespace RealEstate.Api.Endpoints.Identity;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/logout", Handle).RequireAuthorization();
    
    private static async Task<IResult> Handle(SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
}