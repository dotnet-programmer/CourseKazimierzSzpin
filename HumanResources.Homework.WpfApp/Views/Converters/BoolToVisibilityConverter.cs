using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HumanResources.Homework.WpfApp.Views.Converters;

[ValueConversion(typeof(bool), typeof(Visibility))]
internal class BoolToVisibilityConverter : BaseValueConverter<BoolToVisibilityConverter>
{
	public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		=> (System.Convert.ToBoolean(value)) ? Visibility.Visible : Visibility.Collapsed;

	public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		=> (Visibility)value == Visibility.Visible;
}