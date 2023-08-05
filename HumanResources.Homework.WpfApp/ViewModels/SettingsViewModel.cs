using System.Windows.Controls;
using System.Windows.Input;
using HumanResources.Homework.WpfApp.Commands;
using HumanResources.Homework.WpfApp.Models;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal class SettingsViewModel : BaseViewModel
{
	private readonly PasswordBox _passwordBoxSettings;

	public SettingsViewModel(PasswordBox passwordBoxSettings)
	{
		SetCommands();
		_settings = new();
		_passwordBoxSettings = passwordBoxSettings;
		_passwordBoxSettings.Password = Settings.Password;
	}

	#region Property binding

	private UserSettings _settings;
	public UserSettings Settings
	{
		get => _settings;
		set { _settings = value; OnPropertyChanged(); }
	}

	#endregion Property binding

	public ICommand PasswordChangedCommand { get; private set; }
	public ICommand ConfirmCommand { get; private set; }

	private void SetCommands()
	{
		PasswordChangedCommand = new RelayCommand(PasswordChanged);
		ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
	}

	private void PasswordChanged(object commandParameter) => Settings.Password = _passwordBoxSettings.Password;

	private void Confirm(object commandParameter) => Settings.Save();

	private bool CanConfirm(object commandParameter) => Settings.IsValid;
}