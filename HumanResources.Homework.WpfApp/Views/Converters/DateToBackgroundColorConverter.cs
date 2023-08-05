using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HumanResources.Homework.WpfApp.Views.Converters;

[ValueConversion(typeof(DateTime), typeof(SolidColorBrush))]
internal class DateToBackgroundColorConverter : BaseValueConverter<DateToBackgroundColorConverter>
{
	public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		=> value == null ? Brushes.Honeydew : Brushes.LavenderBlush;

	public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}