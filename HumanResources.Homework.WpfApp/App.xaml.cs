using System.Windows;
using HumanResources.Homework.WpfApp.Repositories;

namespace HumanResources.Homework.WpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

	private readonly LogRepository _logRepository = new();


	private async void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
	{
		_logger.Error(e.Exception, e.Exception.Message);
		await _logRepository.LogErrorAsync(System.Security.Principal.WindowsIdentity.GetCurrent().Name, e.Exception);
		MessageBox.Show($"Wystąpił nieoczekiwany wyjątek{Environment.NewLine}{e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
	}
}