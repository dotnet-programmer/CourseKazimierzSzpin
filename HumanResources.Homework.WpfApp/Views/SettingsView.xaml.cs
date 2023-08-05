using System.Windows;
using HumanResources.Homework.WpfApp.ViewModels;

namespace HumanResources.Homework.WpfApp.Views;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : Window
{
	public SettingsView()
	{
		InitializeComponent();
		DataContext = new SettingsViewModel(PasswordBoxSetings);
	}
}