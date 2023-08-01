using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StudentsDiary.WpfApp.ViewModels;

namespace StudentsDiary.WpfApp.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
	public MainWindow()
	{
		InitializeComponent();

		// INFO - MahApps 1 - manager ustawiający schemat kolorów na ten ustawiony w Windowsie
		ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
		ThemeManager.Current.SyncTheme();

		// INFO - Wiązanie DataContext 2 - w pliku code behind
		// INFO - MahApps 6 - dodanie okien dialogowych zgodnych z MVVM
		DataContext = new MainViewModel(DialogCoordinator.Instance);
	}
}