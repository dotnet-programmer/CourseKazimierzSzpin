using System;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace StudentsDiary.WpfApp;

public partial class App : Application
{
	// INFO - Globalna obsługa wyjątków
	private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
	{
		// logowanie do pliku / bazy danych
		var metroWindow = Current.MainWindow as MetroWindow;
		metroWindow.ShowMessageAsync("Nieoczekiwany wyjątek", $"Wystąpił nieoczekiwany wyjątek.{Environment.NewLine}{e.Exception.Message}");
		e.Handled = true;
	}
}