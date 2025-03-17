using Azure;
using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Properties;

namespace RealEstate.Api.Endpoints.Properties;

public class GetPropertyByIdEnpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Properties: Get By Id")
            .WithSummary("Retrieves an existing property")
            .WithOrder(4)
            .Produces<Response<Property?>>();
    
    private static async Task<IResult> HandleAsync(IPropertyHandler handler, Guid id)
    {
        var request = new GetPropertyByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}