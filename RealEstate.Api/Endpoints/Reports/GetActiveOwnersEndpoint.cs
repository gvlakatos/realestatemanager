using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models.Reports;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Reports;

public class GetActiveOwnersEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("reports/GetActiveOwners", HandleAsync)
            .Produces<Response<List<ActiveOwner>?>>();
    }

    private static async Task<IResult> HandleAsync(IReportHandler handler)
    {
        var result = await handler.GetActiveOwnersReportAsync();
        return result.IsSuccess 
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}