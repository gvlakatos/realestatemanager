using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Data;
using RealEstate.Api.Endpoints;
using RealEstate.Api.Handlers;
using RealEstate.Core.Handlers;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(cnnStr);
});

builder.Services.AddTransient<IOwnerHandler, OwnerHandler>();
builder.Services.AddTransient<ITenantHandler, TenantHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapGet("/", () => new { message = "Ok" });
app.MapEndpoints();

app.Run();

