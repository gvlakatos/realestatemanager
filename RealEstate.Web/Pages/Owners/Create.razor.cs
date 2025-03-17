using Microsoft.AspNetCore.Components;
using MudBlazor;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;

namespace RealEstate.Web.Pages.Owners;

public partial class CreateOwnerPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreateOwnerRequest InputModel { get; set; } = new();

    #endregion
    
    #region Service

    [Inject]
    public IOwnerHandler Handler { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion
    
    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/owners");
            }
            else
                Snackbar.Add(result.Message, Severity.Error);
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