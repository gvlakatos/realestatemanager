using MudBlazor;

namespace RealEstate.Web;

public static class Configuration
{
    public const string HttpClientName = "realEstateClient";
    public static string BackendUrl { get; set; } = string.Empty;
    
    public static MudTheme Theme = new MudTheme
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#3C4E69",       // Cor principal (botões, destaques)
            Secondary = "#8BA4D5",      // Cor secundária
            Background = "#FFF7EE",     // Fundo principal da aplicação
            AppbarBackground = "#3C4E69", // Fundo da barra de navegação
            AppbarText = "#FFF7EE",     // Texto da barra de navegação
            TextPrimary = "#3C4E69",    // Cor do texto principal
            DrawerText = "#3C4E69",     // Texto do menu lateral (drawer)
            DrawerBackground = "#8BA4D5" // Fundo do menu lateral
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#8BA4D5",        // Cor principal (botões, destaques)
            Secondary = "#FF9A69",      // Cor secundária
            Background = "#3C4E69",     // Fundo principal da aplicação
            AppbarBackground = "#8BA4D5", // Fundo da barra de navegação
            AppbarText = "#3C4E69",     // Texto da barra de navegação
            TextPrimary = "#FFF7EE",    // Cor do texto principal
            DrawerText = "#FFF7EE",     // Texto do menu lateral (drawer)
            DrawerBackground = "#3C4E69" // Fundo do menu lateral
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Raleway", "sans-serif"]
            }
        }
    };
}