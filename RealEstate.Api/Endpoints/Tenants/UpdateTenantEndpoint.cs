using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Tenants;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Tenants;

public class UpdateTenantEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("{id}", HandleAsync)
            .WithName("Tenants: Update")
            .WithSummary("Update an existing tenant")
            .WithOrder(2)
            .Produces<Response<Tenant?>>();
    
    private static async Task<IResult> HandleAsync(ITenantHandler handler, UpdateTenantRequest request, Guid id)
    {
        request.Id = id;
        
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result.Data) 
            : TypedResults.BadRequest(result.Data);
    }
}