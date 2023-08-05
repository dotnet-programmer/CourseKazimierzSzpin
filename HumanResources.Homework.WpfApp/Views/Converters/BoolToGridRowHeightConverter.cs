using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HumanResources.Homework.WpfApp.Views.Converters;

[ValueConversion(typeof(bool), typeof(GridLength))]
internal class BoolToGridRowHeightConverter : BaseValueConverter<BoolToGridRowHeightConverter>
{
	public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		=> ((bool)value) ? new GridLength(1, GridUnitType.Auto) : new GridLength(0);

	public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}