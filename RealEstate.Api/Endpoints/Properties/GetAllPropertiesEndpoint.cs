using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Commom.Api;
using RealEstate.Core;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Properties;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Properties;

public class GetAllPropertiesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("", HandleAsync)
            .WithName("Properties: Get All")
            .WithSummary("Retrieves all properties")
            .WithOrder(5)
            .Produces<PagedResponse<List<Property>?>>();
    
    private static async Task<IResult> HandleAsync(IPropertyHandler handler, 
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllPropertiesRequest
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