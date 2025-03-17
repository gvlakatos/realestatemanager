using Azure;
using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Properties;

namespace RealEstate.Api.Endpoints.Properties;

public class CreatePropertyEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("", HandleAsync)
            .WithName("Properties: Create")
            .WithSummary("Creates a new property")
            .WithOrder(1)
            .Produces<Response<Property?>>();
    
    private static async Task<IResult> HandleAsync(IPropertyHandler handler, CreatePropertyRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data) 
            : TypedResults.BadRequest(result.Data);
    }
}