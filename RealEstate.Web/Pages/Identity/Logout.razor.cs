using Microsoft.AspNetCore.Components;
using MudBlazor;
using RealEstate.Core.Handlers;
using RealEstate.Web.Security;

namespace RealEstate.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    #region Dependencies
    
    [Inject]
    public IIdentityHandler Handler { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] 
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion
    
    #region Properties
    
    public bool IsBusy { get; set; } = false;
    
    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
        {
            await Handler.LogoutAsync();
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }
        
        await base.OnInitializedAsync();
    }
    
    #endregion
}