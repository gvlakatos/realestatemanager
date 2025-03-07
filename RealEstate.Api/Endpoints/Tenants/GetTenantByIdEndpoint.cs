using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Requests.Tenants;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Tenants;

public class GetTenantByIdEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Tenants: Get By Id")
            .WithSummary("Retrieves an existing tenant")
            .WithOrder(4)
            .Produces<Response<Tenant?>>();
    
    private static async Task<IResult> HandleAsync(ITenantHandler handler, Guid id)
    {
        var request = new GetTenantByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}