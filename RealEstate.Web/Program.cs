using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using RealEstate.Core.Handlers;
using RealEstate.Core.Helpers;
using RealEstate.Web;
using RealEstate.Web.Handlers;
using RealEstate.Web.Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});
builder.Services.AddHttpClient(Configuration.HttpClientName, options =>
{
    options.BaseAddress = new Uri(Configuration.BackendUrl);
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IIdentityHandler, IdentityHandler>();
builder.Services.AddTransient<IOwnerHandler, OwnerHandler>();
builder.Services.AddTransient<ITenantHandler, TenantHandler>();
builder.Services.AddTransient<IPropertyHandler, PropertyHandler>();
builder.Services.AddTransient<IReportHandler, ReportHandler>();

builder.Services.AddLocalization();
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

EnumHelper.Configure(Assembly.GetExecutingAssembly(), "RealEstate.Core.Resources.PropertyStatusResource");

await builder.Build().RunAsync();
