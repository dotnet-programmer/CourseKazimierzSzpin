using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using StudentsDiary.WpfApp.Commands;
using StudentsDiary.WpfApp.Models;

namespace StudentsDiary.WpfApp.ViewModels;

internal class SettingsViewModel : BaseViewModel
{
	private UserSettings _userSettings;

	private string _serverName;
	private readonly bool _canCloseWindow;

	public SettingsViewModel(bool canCloseWindow)
	{
		SetCommands();
		_userSettings = new UserSettings();
		_canCloseWindow = canCloseWindow;
	}

	public UserSettings UserSettings
	{
		get => _userSettings;
		set { _userSettings = value; OnPropertyChanged(); }
	}

	public string ServerName
	{
		get => _serverName;
		set { _serverName = value; OnPropertyChanged(); }
	}

	public ICommand ConfirmCommand { get; set; }

	public ICommand CloseCommand { get; set; }

	private void SetCommands()
	{
		ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
		CloseCommand = new RelayCommand(Close);
	}

	private void Confirm(object obj)
	{
		UserSettings.Save();
		RestartApplication();
	}

	private bool CanConfirm(object obj) => UserSettings.IsValid;

	private void Close(object obj)
	{
		if (_canCloseWindow)
		{
			(obj as Window).Close();
		}
		else
		{
			Application.Current.Shutdown();
		}
	}

	private void RestartApplication()
	{
		var appLocation = Application.ResourceAssembly.Location;
		Process.Start(appLocation.EndsWith(".dll") ? appLocation.Replace(".dll", ".exe") : appLocation);
		Application.Current.Shutdown();
	}
}