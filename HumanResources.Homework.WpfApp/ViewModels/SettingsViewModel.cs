using System.Windows.Controls;
using System.Windows.Input;
using HumanResources.Homework.WpfApp.Commands;
using HumanResources.Homework.WpfApp.Models;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal class SettingsViewModel : BaseViewModel
{
	private readonly PasswordBox _passwordBox;

	public SettingsViewModel(PasswordBox passwordBox)
	{
		SetCommands();
		_settings = new();
		_passwordBox = passwordBox;
		_passwordBox.Password = Settings.Password;
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

	private void PasswordChanged(object commandParameter)
		=> Settings.Password = _passwordBox.Password;

	private void Confirm(object commandParameter)
		=> Settings.Save();

	private bool CanConfirm(object commandParameter)
		=> Settings.IsValid;
}