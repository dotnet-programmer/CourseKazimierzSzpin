using System.Windows;
using HumanResources.Homework.WpfApp.ViewModels;

namespace HumanResources.Homework.WpfApp.Views;

public partial class SettingsView : Window
{
	public SettingsView()
	{
		InitializeComponent();
		DataContext = new SettingsViewModel(PasswordBoxSetings);
	}
}