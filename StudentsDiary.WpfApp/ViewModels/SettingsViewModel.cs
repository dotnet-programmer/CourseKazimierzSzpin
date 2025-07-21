using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using StudentsDiary.WpfApp.Commands;
using StudentsDiary.WpfApp.Models;

namespace StudentsDiary.WpfApp.ViewModels;

internal class SettingsViewModel : BaseViewModel
{
	private readonly bool _canCloseWindow;

	private UserSettings _userSettings;

	public SettingsViewModel(bool canCloseWindow)
	{
		SetCommands();
		_userSettings = new();
		_canCloseWindow = canCloseWindow;
	}

	public UserSettings UserSettings
	{
		get => _userSettings;
		set { _userSettings = value; OnPropertyChanged(); }
	}

	public ICommand ConfirmCommand { get; private set; }
	public ICommand CloseCommand { get; private set; }

	private void SetCommands()
	{
		ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
		CloseCommand = new RelayCommand(Close);
	}

	private void Confirm(object commandParameter)
	{
		UserSettings.Save();
		RestartApplication();
	}

	private bool CanConfirm(object commandParameter)
		=> UserSettings.IsValid;

	private void Close(object commandParameter)
	{
		if (_canCloseWindow)
		{
			(commandParameter as Window).Close();
		}
		else
		{
			Application.Current.Shutdown();
		}
	}

	private static void RestartApplication()
	{
		Process.Start(Application.ResourceAssembly.Location.Replace(".dll", ".exe"));
		Application.Current.Shutdown();
	}
}