using Azure;
using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Properties;

namespace RealEstate.Api.Endpoints.Properties;

public class DeletePropertyEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Properties: Delete")
            .WithSummary("Delete an existing property")
            .WithOrder(3)
            .Produces<Response<Property?>>();
    
    private static async Task<IResult> HandleAsync(IPropertyHandler handler, Guid id)
    {
        var request = new DeletePropertyRequest
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}