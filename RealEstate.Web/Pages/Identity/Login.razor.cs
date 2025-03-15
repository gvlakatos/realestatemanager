using Microsoft.AspNetCore.Components;
using MudBlazor;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Identity;
using RealEstate.Web.Security;

namespace RealEstate.Web.Pages.Identity;

public partial class LoginPage : ComponentBase
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
    public LoginRequest InputModel { get; set; } = new();
    
    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        
        if (user.Identity is not null && user.Identity.IsAuthenticated)
            NavigationManager.NavigateTo("/");
    }
    
    #endregion
    
    #region Methods
    
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            
            var result = await Handler.LoginAsync(InputModel);

            if (result.IsSuccess)
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo("/"); 
                Snackbar.Add(result.Message, Severity.Success);
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false; 
        }
    }
    
    #endregion
}