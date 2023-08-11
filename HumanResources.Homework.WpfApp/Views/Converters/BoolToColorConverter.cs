using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HumanResources.Homework.WpfApp.Views.Converters;

// INFO - MVVM Konwertery 1 - klasa konwertera, obiekt deklarowany w zasobach okna
//[ValueConversion(typeof(bool), typeof(SolidColorBrush))]
//internal class BoolToColorConverter : IValueConverter
//{
//	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//		=> System.Convert.ToBoolean(value) ? Brushes.ForestGreen : Brushes.IndianRed;

//	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
//}

// INFO - MVVM Konwertery 2 - konwerter dziedziczący po klasie bazowej, nie trzeba deklarować obiektów w XAML
[ValueConversion(typeof(bool), typeof(SolidColorBrush))]
internal class BoolToColorConverter : BaseValueConverter<BoolToColorConverter>
{
	public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		=> System.Convert.ToBoolean(value) ? Brushes.ForestGreen : Brushes.IndianRed;

	public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}