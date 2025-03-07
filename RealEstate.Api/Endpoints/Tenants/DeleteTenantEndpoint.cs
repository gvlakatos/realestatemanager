using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Requests.Tenants;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Tenants;

public class DeleteTenantEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Tenants: Delete")
            .WithSummary("Delete an existing tenant")
            .WithOrder(3)
            .Produces<Response<Tenant?>>();
    
    private static async Task<IResult> HandleAsync(ITenantHandler handler, Guid id)
    {
        var request = new DeleteTenantRequest
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}