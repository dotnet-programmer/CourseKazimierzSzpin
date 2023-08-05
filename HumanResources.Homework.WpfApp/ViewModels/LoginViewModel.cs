using System.Timers;
using System.Windows;
using System.Windows.Input;
using HumanResources.Homework.WpfApp.Commands;
using HumanResources.Homework.WpfApp.Models;
using HumanResources.Homework.WpfApp.Models.Wrappers;
using HumanResources.Homework.WpfApp.Repositories;
using HumanResources.Homework.WpfApp.Views;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal class LoginViewModel : BaseViewModel
{
	private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
	private static readonly string WindowsUserName = $@"{Environment.MachineName}\\{Environment.UserDomainName}\\{Environment.UserName} - {System.Security.Principal.WindowsIdentity.GetCurrent().Name}";
	private readonly ConnectionStatus _connected = new() { StatusText = "Połączono z bazą danych!", IsValidConnection = true };
	private readonly ConnectionStatus _disconnected = new() { StatusText = "Błąd połączenia!", IsValidConnection = false };
	private readonly System.Timers.Timer _checkConnectionTimer = new(5000);

	public LoginViewModel()
	{
		StringCipherHelper.EncryptStringFromConfigAndSave("Password");
		SetCommands();
		SetConnectionStatus();
		_checkConnectionTimer.Elapsed += UnlockRefreshButton;
		_isCheckConnectionEnabled = true;
		_user = new();
	}

	#region Property binding

	private UserWrapper _user;
	public UserWrapper User
	{
		get => _user;
		set { _user = value; OnPropertyChanged(); }
	}

	private ConnectionStatus _status;
	public ConnectionStatus Status
	{
		get => _status;
		set { _status = value; OnPropertyChanged(); }
	}

	private bool _isCheckConnectionEnabled;
	public bool IsCheckConnectionEnabled
	{
		get => _isCheckConnectionEnabled;
		set { _isCheckConnectionEnabled = value; OnPropertyChanged(); }
	}

	#endregion Property binding

	public ICommand ShowSettingsCommand { get; private set; }
	public ICommand CheckConnectionCommand { get; private set; }
	public ICommand LoginCommand { get; private set; }
	public ICommand CloseAppCommand { get; private set; }

	private void SetCommands()
	{
		ShowSettingsCommand = new RelayCommand(ShowSettings);
		CheckConnectionCommand = new RelayCommand(CheckConnection);
		LoginCommand = new RelayCommand(Login, CanLogin);
		CloseAppCommand = new RelayCommand(CloseApp);
	}

	private void ShowSettings(object commandParameter)
	{
		SettingsView settingsView = new();
		bool? dialogResult = settingsView.ShowDialog();
		if (dialogResult.HasValue && dialogResult.Value)
		{
			SetConnectionStatus();
		}
	}

	private void CheckConnection(object commandParameter)
	{
		IsCheckConnectionEnabled = false;
		_checkConnectionTimer.Start();
		SetConnectionStatus();
		if (!Status.IsValidConnection)
		{
			Logger.Info($"{Status.StatusText} - user: {User.Name}, użytkownik: {WindowsUserName}");
		}
	}

	private void Login(object commandParameter)
	{
		LoginParams loginParams = (LoginParams)commandParameter;

		if (DbRepository.LoginToApplication(User, loginParams.PasswordBox.Password))
		{
			MainView mainView = new();
			loginParams.Window.Close();
			mainView.ShowDialog();
		}
		else
		{
			Logger.Info($"Błąd logowania - user: {User.Name}, użytkownik: {WindowsUserName}");
			MessageBox.Show("Podałeś złe dane do logowania!", "Błąd logowania!", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	private bool CanLogin(object commandParameter) => User.IsValid && Status.IsValidConnection;

	private void CloseApp(object commandParameter) => Application.Current.Shutdown();

	private void SetConnectionStatus() => Status = DbRepository.IsValidConnectionToDataBase() ? _connected : _disconnected;

	private void UnlockRefreshButton(object? sender, ElapsedEventArgs e)
	{
		IsCheckConnectionEnabled = true;
		_checkConnectionTimer.Stop();
	}
}