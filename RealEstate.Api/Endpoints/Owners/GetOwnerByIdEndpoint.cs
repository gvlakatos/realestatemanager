using RealEstate.Api.Commom.Api;
using RealEstate.Core.Models;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Owners;

public class GetOwnerByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Owners: Get By Id")
            .WithSummary("Retrieves an existing owner")
            .WithOrder(4)
            .Produces<Response<Owner?>>();
    
    private static async Task<IResult> HandleAsync(IOwnerHandler handler, Guid id)
    {
        var request = new GetOwnerByIdRequest()
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}