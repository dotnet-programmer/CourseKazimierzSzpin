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
					_isServerAddressValid = !string.IsNullOrWhiteSpace(ServerAddress);
					Error = _isServerAddressValid ? string.Empty : "Pole Adres serwera jest wymagane.";
					break;
				case nameof(ServerName):
					_isServerNameValid = !string.IsNullOrWhiteSpace(ServerName);
					Error = _isServerNameValid ? string.Empty : "Pole Nazwa serwera jest wymagane.";
					break;
				case nameof(Database):
					_isDatabaseValid = !string.IsNullOrWhiteSpace(Database);
					Error = _isDatabaseValid ? string.Empty : "Pole Baza danych jest wymagane.";
					break;
				case nameof(User):
					_isUserValid = !string.IsNullOrWhiteSpace(User);
					Error = _isUserValid ? string.Empty : "Pole Użytkownik jest wymagane.";
					break;
				case nameof(Password):
					_isPasswordValid = !string.IsNullOrWhiteSpace(Password);
					Error = _isPasswordValid ? string.Empty : "Pole Hasło jest wymagane.";
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