using RealEstate.Api.Commom.Api;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models.Reports;
using RealEstate.Core.Requests.Reports;
using RealEstate.Core.Responses;

namespace RealEstate.Api.Endpoints.Reports;

public class GetActiveTenantsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("reports/GetActiveTenants", HandleAsync)
            .Produces<Response<List<ActiveTenant>?>>();
    }

    private static async Task<IResult> HandleAsync(IReportHandler handler)
    {
        var result = await handler.GetActiveTenantsReportAsync();
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}