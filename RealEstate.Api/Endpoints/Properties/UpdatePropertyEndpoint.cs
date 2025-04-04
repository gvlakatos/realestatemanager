using Azure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Properties;

namespace RealEstate.Api.Endpoints.Properties;

public class UpdatePropertyEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Properties: Update")
            .WithSummary("Updates an existing property")
            .WithOrder(2)
            .Produces<Response<Property?>>();
    
    private static async Task<IResult> HandleAsync(IPropertyHandler handler, UpdatePropertyRequest request, Guid id)
    {
        request.Id = id;
        
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}