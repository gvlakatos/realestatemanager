using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Tenants;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Tenants;

public class CreateTenantEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("", HandleAsync)
            .WithName("Tenants: Create")
            .WithSummary("Creates a new tenant")
            .WithOrder(1)
            .Produces<Response<Tenant?>>();

    private static async Task<IResult> HandleAsync(ITenantHandler handler, CreateTenantRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data) 
            : TypedResults.BadRequest(result.Data);
    }
}