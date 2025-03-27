using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace RealEstate.Core.Helpers;

public class EnumHelper
{
    private static ResourceManager _resourceManager;

    public static void Configure(Assembly assembly, string resourceBaseName)
    {
        _resourceManager = new ResourceManager(resourceBaseName, assembly);
    }

    public static string GetEnumDescription<TEnum>(Enum value) where TEnum : Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        
        if (field is null)
            return value.ToString();
        
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        
        if (attribute is not null)
            return attribute.Description;

        if (_resourceManager is not null)
        {
            string resourceKey = $"{typeof(TEnum).Name}_{value}";
            string localizedString = _resourceManager.GetString(resourceKey, CultureInfo.CurrentCulture);
            if (!string.IsNullOrEmpty(localizedString))
                return localizedString;
        }
        
        return value.ToString();
    }
}