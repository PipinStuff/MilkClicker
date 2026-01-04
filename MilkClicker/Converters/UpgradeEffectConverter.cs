using System.Globalization;

namespace MilkClicker.Converters;

public class UpgradeEffectConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Upgrade upgrade)
        {
            return $"+{upgrade.BaseValue:F1} per level";
        }
        return "N/A";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
