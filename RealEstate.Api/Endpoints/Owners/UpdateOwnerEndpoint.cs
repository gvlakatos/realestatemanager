using RealEstate.Api.Commom.Api;
using RealEstate.Core.Models;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Owners;

public class UpdateOwnerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Owners: Update")
            .WithSummary("Update an existing owner")
            .WithOrder(2)
            .Produces<Response<Owner?>>();
    
    private static async Task<IResult> HandleAsync(IOwnerHandler handler, UpdateOwnerRequest request, Guid id)
    {
        request.Id = id;
        
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result.Data) 
            : TypedResults.BadRequest(result.Data);
    }
}