using Microsoft.AspNetCore.Components;
using MudBlazor;
using RealEstate.Core.Handlers;
using RealEstate.Core.Models;
using RealEstate.Core.Requests.Tenants;

namespace RealEstate.Web.Pages.Tenants;

public partial class ListTenantsPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Tenant> Tenants { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;

    #endregion
    
    #region Service

    [Inject]
    public ITenantHandler Handler { get; set; } = null!;
    
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
            var request = new GetAllTenantsRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Tenants = result.Data ?? [];
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

    public Func<Tenant, bool> Filter => tenant =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        if (tenant.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        
        if (tenant.CpfCnpj.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        
        if (tenant.Email.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };

    public async void onDeleteButtonClicked(Guid id, string Title)
    {
        var result = await DialogService.ShowMessageBox(
            "Atenção",
            $"Ao prosseguir, o cadastro do proprietário {Title} será excluído. Esta ação é irreversível! Deseja continuar?",
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
            await Handler.DeleteAsync(new DeleteTenantRequest() { Id = id });
            Tenants.RemoveAll(x => x.Id == id);
            Snackbar.Add("Registro excluido com sucesso!", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    #endregion
}