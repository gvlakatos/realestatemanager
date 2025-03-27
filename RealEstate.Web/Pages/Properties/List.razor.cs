using Microsoft.AspNetCore.Components;
using MudBlazor;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Properties;

namespace RealEstate.Web.Pages.Properties;

public partial class ListPropertiesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Property> Properties { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;

    #endregion
    
    #region Service

    [Inject]
    public IPropertyHandler Handler { get; set; } = null!;
    
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
            var request = new GetAllPropertiesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Properties = result.Data ?? [];
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

    public Func<Property, bool> Filter => property =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        if (property.Address.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        
        if (property.PropertyStatus.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        
        if (property.TransactionType.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };

    public async void onDeleteButtonClicked(Guid id, string Title)
    {
        var result = await DialogService.ShowMessageBox(
            "Atenção",
            $"Ao prosseguir, o cadastro do imóvel será excluído. Esta ação é irreversível! Deseja continuar?",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id);
        
        StateHasChanged();
    }

    public async Task OnDeleteAsync(Guid id)
    {
        try
        {
            await Handler.DeleteAsync(new DeletePropertyRequest { Id = id });
            Properties.RemoveAll(x => x.Id == id);
            Snackbar.Add("Registro excluido com sucesso!", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    #endregion
}