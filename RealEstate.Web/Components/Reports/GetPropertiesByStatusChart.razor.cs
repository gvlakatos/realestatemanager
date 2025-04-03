using Microsoft.AspNetCore.Components;
using MudBlazor;
using RealEstate.Core.Enums;
using RealEstate.Core.Handlers;

namespace RealEstate.Web.Components.Reports;

public partial class GetPropertiesByStatusChartComponent : ComponentBase
{
    #region Properties

    public List<double> Data { get; set; } = [];
    public List<string> Labels { get; set; } = [];
    
    public double Total => Data.Sum();

    #endregion
    
    #region Services

    [Inject]
    public IReportHandler Handler { get; set; } = null!;
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        await GetPropertiesByStatusAsync();
    }

    #endregion
    
    #region Methods

    private async Task GetPropertiesByStatusAsync()
    {
        var result = await Handler.GetPropertiesByStatusReportAsync();
        if (result.IsSuccess is false || result.Data is null)
        {
            Snackbar.Add($"There was an error getting the report.", Severity.Error);
            return;
        }

        foreach (var item in result.Data)
        {
            Labels.Add($"{Enum.GetName(typeof(EPropertyStatus), item.Status)} ({item.Count.ToString()})");
            Data.Add(item.Count);
        }
        Labels.Add($"Total: {Total}");
        Data.Add(0);
    }
    
    #endregion
}