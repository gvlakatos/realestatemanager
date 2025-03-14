using System.Security.Claims;
using RealEstate.Api.Commom.Api;

namespace RealEstate.Api.Endpoints.Identity;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/roles", Handle).RequireAuthorization();
    
    private static async Task<IResult> Handle(ClaimsPrincipal user)
    {
        if (user.Identity is null || user.Identity.IsAuthenticated is false)
            return Results.Unauthorized();
                
        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType).Select(c => new
        {
            c.Issuer,
            c.OriginalIssuer,
            c.Type,
            c.Value,
            c.ValueType
        });
                
        return TypedResults.Json(roles);
    }
}