using System.Globalization;

namespace MilkClicker.Converters;

public class UpgradeCostConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Upgrade upgrade)
        {
            return $"{upgrade.GetNextCost():F0} points";
        }
        return "N/A";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
