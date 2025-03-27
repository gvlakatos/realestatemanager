using System.Reflection;
using RealEstate.Api.Commom.Api;
using RealEstate.Api.Endpoints;
using RealEstate.Core;
using RealEstate.Core.Helpers;

var builder = WebApplication.CreateBuilder(args);
// Necessário manter na ordem abaixo
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

// EnumHelper.Configure(Assembly.GetExecutingAssembly(), "RealEstate.Core.Resources.PropertyStatusResource");

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(Configuration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();

