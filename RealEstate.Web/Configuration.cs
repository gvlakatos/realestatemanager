using MudBlazor;

namespace RealEstate.Web;

public static class Configuration
{
    public static MudTheme Theme = new MudTheme
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#3C4E69",
            Secondary = "#8BA4D5",
            Background = Colors.Gray.Lighten4,
            AppbarBackground = "#FFF7EE",
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            DrawerText = Colors.Shades.Black,
            DrawerBackground = "#FF9A69"
        },
        PaletteDark = new PaletteDark
        {
            Primary  = Colors.Orange.Darken2,
            Secondary = Colors.Shades.White,
            AppbarBackground = Colors.Orange.Darken2,
            AppbarText = Colors.Shades.Black
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