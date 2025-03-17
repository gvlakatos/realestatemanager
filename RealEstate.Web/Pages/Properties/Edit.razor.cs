using Microsoft.AspNetCore.Components;
using MudBlazor;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;
using RealEstate.Core.Requests.Properties;

namespace RealEstate.Web.Pages.Properties;

public partial class EditPropertyPage : ComponentBase
{
    #region Parameters

    [Parameter]
    public string Id { get; set; }

    #endregion
    
    #region Properties

    public bool IsBusy { get; set; } = false;
    public UpdatePropertyRequest InputModel { get; set; } = new();

    #endregion
    
    #region Services

    [Inject]
    public IPropertyHandler Handler { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        var request = new GetPropertyByIdRequest()
        {
            Id = Guid.Parse(Id)
        };

        try
        {
            var response = await Handler.GetByIdAsync(request);
            if (response.IsSuccess && response.Data is not null)
                InputModel = new UpdatePropertyRequest()
                {
                    Id = response.Data.Id,
                    Address = response.Data.Address,
                    PropertyType = response.Data.PropertyType,
                    TransactionType = response.Data.TransactionType,
                    PropertyStatus = response.Data.PropertyStatus,
                    Description = response.Data.Description,
                    OwnerId = response.Data.OwnerId
                };
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
            var result = await Handler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add("Cadastro atualizado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/owners");
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