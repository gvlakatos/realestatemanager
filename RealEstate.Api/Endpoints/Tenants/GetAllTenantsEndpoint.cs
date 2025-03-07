using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Commom.Api;
using RealEstate.Core;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Requests.Tenants;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Tenants;

public class GetAllTenantsEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("", HandleAsync)
            .WithName("Tenants: Get All")
            .WithSummary("Retrieves all tenants")
            .WithOrder(5)
            .Produces<PagedResponse<List<Tenant>?>>();
    
    private static async Task<IResult> HandleAsync(ITenantHandler handler, 
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllTenantsRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}