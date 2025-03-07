using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Commom.Api;
using RealEstate.Core;
using RealEstate.Core.Models;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Owners;

public class GetAllOwnersEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("", HandleAsync)
            .WithName("Owners: Get All")
            .WithSummary("Retrieves all owners")
            .WithOrder(5)
            .Produces<PagedResponse<List<Owner>?>>();
    
    private static async Task<IResult> HandleAsync(IOwnerHandler handler, 
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllOwnersRequest()
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