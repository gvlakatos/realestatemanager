using System.Security.Claims;
using RealEstate.Api.Commom.Api;
using RealEstate.Core.Models.Identity;

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
        var roles = identity.FindAll(identity.RoleClaimType).Select(c => new RoleClaim
        {
            Issuer = c.Issuer,
            OriginalIssuer = c.OriginalIssuer,
            Type = c.Type,
            Value = c.Value,
            ValueType = c.ValueType
        });
                
        return TypedResults.Json(roles);
    }
}