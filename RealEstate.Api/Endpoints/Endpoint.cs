using RealEstate.Api.Commom.Api;
using RealEstate.Api.Endpoints.Identity;
using RealEstate.Api.Endpoints.Owners;
using RealEstate.Api.Endpoints.Properties;
using RealEstate.Api.Endpoints.Tenants;
using RealEstate.Api.Models;

namespace RealEstate.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");
        
        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "Ok" });

        endpoints.MapGroup("v1/owners")
            .WithTags("Owners")
            .RequireAuthorization()
            .MapEndpoint<CreateOwnerEndpoint>()
            .MapEndpoint<UpdateOwnerEndpoint>()
            .MapEndpoint<DeleteOwnerEndpoint>()
            .MapEndpoint<GetOwnerByIdEndpoint>()
            .MapEndpoint<GetAllOwnersEndpoint>();
        
        endpoints.MapGroup("v1/tenants")
            .WithTags("Tenants")
            .RequireAuthorization()
            .MapEndpoint<CreateTenantEndpoint>()
            .MapEndpoint<UpdateTenantEndpoint>()
            .MapEndpoint<DeleteTenantEndpoint>()
            .MapEndpoint<GetTenantByIdEndpoint>()
            .MapEndpoint<GetAllTenantsEndpoint>();

        endpoints.MapGroup("v1/properties")
            .WithTags("Properties")
            .RequireAuthorization()
            .MapEndpoint<CreatePropertyEndpoint>()
            .MapEndpoint<UpdatePropertyEndpoint>()
            .MapEndpoint<DeletePropertyEndpoint>()
            .MapEndpoint<GetPropertyByIdEnpoint>()
            .MapEndpoint<GetAllPropertiesEndpoint>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}