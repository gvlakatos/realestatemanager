using Azure;
using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models.Reports;

namespace RealEstate.Api.Endpoints.Reports;

public class GetPropertiesByStatusEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/getPropertiesByStatus", HandleAsync)
            .Produces<Response<List<PropertiesByStatus>?>>();
    }

    private static async Task<IResult> HandleAsync(IReportHandler handler)
    {
        var result = await handler.GetPropertiesByStatusReportAsync();
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}