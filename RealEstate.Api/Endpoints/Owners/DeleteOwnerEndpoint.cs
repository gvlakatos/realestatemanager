using RealEstate.Api.Commom.Api;
using RealEstate.Core.Models;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Owners;

public class DeleteOwnerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Owners: Delete")
            .WithSummary("Delete an existing owner")
            .WithOrder(3)
            .Produces<Response<Owner?>>();
    
    private static async Task<IResult> HandleAsync(IOwnerHandler handler, Guid id)
    {
        var request = new DeleteOwnerRequest
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}