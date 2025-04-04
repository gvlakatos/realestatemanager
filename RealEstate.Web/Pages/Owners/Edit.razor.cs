using Microsoft.AspNetCore.Components;
using MudBlazor;
using RealEstate.Core.Handlers;
using RealEstate.Core.Requests.Owners;

namespace RealEstate.Web.Pages.Owners;

public partial class EditOwnerPage : ComponentBase
{
    #region Parameters

    [Parameter]
    public string Id { get; set; }

    #endregion
    
    #region Properties

    public bool IsBusy { get; set; } = false;
    public UpdateOwnerRequest InputModel { get; set; } = new();

    #endregion
    
    #region Services

    [Inject]
    public IOwnerHandler Handler { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        var request = new GetOwnerByIdRequest
        {
            Id = Guid.Parse(Id)
        };

        try
        {
            var response = await Handler.GetByIdAsync(request);
            if (response.IsSuccess && response.Data is not null)
                InputModel = new UpdateOwnerRequest
                {
                    Id = response.Data.Id,
                    Name = response.Data.Name,
                    CpfCnpj = response.Data.CpfCnpj,
                    PhoneNumber = response.Data.PhoneNumber,
                    Email = response.Data.Email
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