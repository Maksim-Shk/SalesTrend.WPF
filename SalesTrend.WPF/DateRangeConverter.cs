using System;
using System.Globalization;
using System.Windows.Data;

namespace SalesTrend.WPF;

public class DateRangeConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is DateTime startDate && values[1] is DateTime endDate)
        {
            return Tuple.Create(startDate, endDate);
        }

        return Tuple.Create(DateTime.MinValue, DateTime.MinValue);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
