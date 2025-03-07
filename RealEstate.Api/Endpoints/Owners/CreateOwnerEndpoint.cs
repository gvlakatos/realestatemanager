using RealEstate.Api.Commom.Api;
using RealEstate.Core.Models;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Owners;

public class CreateOwnerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("", HandleAsync)
            .WithName("Owners: Create")
            .WithSummary("Creates a new owner")
            .WithOrder(1)
            .Produces<Response<Owner?>>();

    private static async Task<IResult> HandleAsync(IOwnerHandler handler, CreateOwnerRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Created($"/{result.Data?.Id}", result.Data) 
            : TypedResults.BadRequest(result.Data);
    }
}