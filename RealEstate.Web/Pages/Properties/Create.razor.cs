using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Requests.Properties;
using RealEstate.Web.Components;

namespace RealEstate.Web.Pages.Properties;

public partial class CreatePropertyPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreatePropertyRequest InputModel { get; set; } = new();
    public List<Owner> Owners { get; set; } = [];
    private Guid? SelectedOwnerId;
    public string? SelectedOwnerName;

    #endregion
    
    #region Service

    [Inject]
    public IPropertyHandler Handler { get; set; } = null!;
    
    [Inject]
    public IOwnerHandler OwnerHandler { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllOwnersRequest();
            var result = await OwnerHandler.GetAllAsync(request);

            if (result.IsSuccess)
                Owners = result.Data ?? [];
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
    
    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            InputModel.OwnerId = (Guid)SelectedOwnerId;
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/properties");
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

    public async Task onSearchOwnerButtonClicked()
    {
        var parameters = new DialogParameters<OwnerSelectionDialog>();
        var dialog = await DialogService.ShowAsync<OwnerSelectionDialog>("Selecionar Proprietário", parameters);
        var result =  await dialog.Result;
        
        if (result?.Data is not null)
        {
            SelectedOwnerId = result.Data.As<Owner>().Id;
            SelectedOwnerName = result.Data.As<Owner>().Name;
        }
    }
    #endregion
}