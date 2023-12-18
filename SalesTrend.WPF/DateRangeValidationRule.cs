using System;
using System.Globalization;
using System.Windows.Controls;

namespace SalesTrend.WPF;

public class DateRangeValidationRule : ValidationRule
{
    public DateTime MinDate { get; set; } = new DateTime(1970, 1, 1);
    public DateTime MaxDate { get; set; } = DateTime.Now;

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        DateTime date;
        if (value != null && DateTime.TryParse(value.ToString(), out date))
        {
            if (date >= MinDate && date <= MaxDate)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, $"Дата должна быть в промежутке между {MinDate:dd.MM.yyyy} и {MaxDate:dd.MM.yyyy}.");
            }
        }
        return new ValidationResult(false, "Неверный формат даты.");
    }
}
