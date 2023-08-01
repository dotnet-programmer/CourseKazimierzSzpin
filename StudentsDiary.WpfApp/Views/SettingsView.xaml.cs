using MahApps.Metro.Controls;
using StudentsDiary.WpfApp.ViewModels;

namespace StudentsDiary.WpfApp.Views;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : MetroWindow
{
	public SettingsView(bool canCloseWindow)
	{
		InitializeComponent();
		DataContext = new SettingsViewModel(canCloseWindow);
	}
}