using System.ComponentModel;
using StudentsDiary.WpfApp.Properties;

namespace StudentsDiary.WpfApp.Models;

public class UserSettings : IDataErrorInfo
{
	private bool _isServerAddressValid;
	private bool _isServerNameValid;
	private bool _isDatabaseValid;
	private bool _isUserValid;
	private bool _isPasswordValid;

	public string ServerAddress
	{
		get => Settings.Default.ServerAddress;
		set => Settings.Default.ServerAddress = value;
	}

	public string ServerName
	{
		get => Settings.Default.ServerName;
		set => Settings.Default.ServerName = value;
	}

	public string Database
	{
		get => Settings.Default.Database;
		set => Settings.Default.Database = value;
	}

	public string User
	{
		get => Settings.Default.User;
		set => Settings.Default.User = value;
	}

	public string Password
	{
		get => Settings.Default.Password;
		set => Settings.Default.Password = value;
	}

	public bool IsValid
		=> _isServerAddressValid && _isServerNameValid && _isDatabaseValid && _isUserValid && _isPasswordValid;

	public string Error { get; set; }

	public string this[string columnName]
	{
		get
		{
			switch (columnName)
			{
				case nameof(ServerAddress):
					bool isErrorServerAddress = string.IsNullOrWhiteSpace(ServerAddress);
					Error = isErrorServerAddress ? "Pole Adres serwera jest wymagane." : string.Empty;
					_isServerAddressValid = !isErrorServerAddress;
					break;
				case nameof(ServerName):
					bool isErrorServerName = string.IsNullOrWhiteSpace(ServerName);
					Error = isErrorServerName ? "Pole Nazwa serwera jest wymagane." : string.Empty;
					_isServerNameValid = !isErrorServerName;
					break;
				case nameof(Database):
					bool isErrorDatabase = string.IsNullOrWhiteSpace(Database);
					Error = isErrorDatabase ? "Pole Baza danych jest wymagane." : string.Empty;
					_isDatabaseValid = !isErrorDatabase;
					break;
				case nameof(User):
					bool isErrorUser = string.IsNullOrWhiteSpace(User);
					Error = isErrorUser ? "Pole Użytkownik jest wymagane." : string.Empty;
					_isUserValid = !isErrorUser;
					break;
				case nameof(Password):
					bool isErrorPassword = string.IsNullOrWhiteSpace(Password);
					Error = isErrorPassword ? "Pole Hasło jest wymagane." : string.Empty;
					_isPasswordValid = !isErrorPassword;
					break;
				default:
					break;
			}
			return Error;
		}
	}

	internal void Save()
		=> Settings.Default.Save();
}