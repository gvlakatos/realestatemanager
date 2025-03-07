using RealEstate.Api.Commom.Api;
using RealEstate.Api.Endpoints.Owners;
using RealEstate.Api.Endpoints.Tenants;

namespace RealEstate.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("v1/owners")
            .WithTags("Owners")
            //.RequireAuthorization()
            .MapEndpoint<CreateOwnerEndpoint>()
            .MapEndpoint<UpdateOwnerEndpoint>()
            .MapEndpoint<DeleteOwnerEndpoint>()
            .MapEndpoint<GetOwnerByIdEndpoint>()
            .MapEndpoint<GetAllOwnersEndpoint>();
        
        endpoints.MapGroup("v1/tenants")
            .WithTags("Tenants")
            //.RequireAuthorization()
            .MapEndpoint<CreateTenantEndpoint>()
            .MapEndpoint<UpdateTenantEndpoint>()
            .MapEndpoint<DeleteTenantEndpoint>()
            .MapEndpoint<GetTenantByIdEndpoint>()
            .MapEndpoint<GetAllTenantsEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}