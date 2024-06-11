using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using HumanResources.Homework.WpfApp.Models;

namespace HumanResources.Homework.WpfApp.Views.Converters;

internal class LoginParamsConverter : BaseMultiValueConverter<LoginParamsConverter>
{
	public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		=> new LoginParams
		{
			Window = values[0] as Window,
			PasswordBox = values[1] as PasswordBox
		};

	public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		=> throw new NotImplementedException();
}