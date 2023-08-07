using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace HumanResources.Homework.WpfApp.Models;

public class UserSettings : IDataErrorInfo
{
	private bool _isServerAddressValid;
	private bool _isServerNameValid;
	private bool _isDatabaseValid;
	private bool _isUserValid;

	public UserSettings()
	{
		try
		{
			ServerAddress = GetStringFromConfig(nameof(ServerAddress));
			ServerName = GetStringFromConfig(nameof(ServerName));
			Database = GetStringFromConfig(nameof(Database));
			User = GetStringFromConfig(nameof(User));
			Password = StringCipherHelper.DecryptStringFromConfig(nameof(Password));
		}
		catch (ConfigurationErrorsException ex)
		{
			System.Windows.MessageBox.Show(ex.Message);
		}
	}

	public string ServerAddress { get; set; }
	public string ServerName { get; set; }
	public string Database { get; set; }
	public string User { get; set; }
	public string Password { get; set; }

	public bool IsValid => _isServerAddressValid && _isServerNameValid && _isDatabaseValid && _isUserValid && !string.IsNullOrWhiteSpace(Password);

	private string GetRequiredFieldErrorMessage(bool isValid, string fieldName) => isValid ? string.Empty : $"Pole {fieldName} jest wymagane.";

	public static string GetStringFromConfig(string key) => ConfigurationManager.AppSettings[key] ?? "Not Found";

	public void Save()
	{
		var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		var settings = configFile.AppSettings.Settings;
		settings[nameof(ServerAddress)].Value = ServerAddress;
		settings[nameof(ServerName)].Value = ServerName;
		settings[nameof(Database)].Value = Database;
		settings[nameof(User)].Value = User;
		settings[nameof(Password)].Value = StringCipherHelper.EncryptString(Password);
		configFile.Save();
		ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
	}

	#region IDataErrorInfo Members

	public string Error { get; set; }

	public string this[string columnName]
	{
		get
		{
			switch (columnName)
			{
				case nameof(ServerAddress):
					_isServerAddressValid = !string.IsNullOrWhiteSpace(ServerAddress);
					Error = GetRequiredFieldErrorMessage(_isServerAddressValid, "Adres serwera");
					break;
				case nameof(ServerName):
					_isServerNameValid = !string.IsNullOrWhiteSpace(ServerName);
					Error = GetRequiredFieldErrorMessage(_isServerNameValid, "Nazwa serwera");
					break;
				case nameof(Database):
					_isDatabaseValid = !string.IsNullOrWhiteSpace(Database);
					Error = GetRequiredFieldErrorMessage(_isDatabaseValid, "Baza danych");
					break;
				case nameof(User):
					_isUserValid = !string.IsNullOrWhiteSpace(User);
					Error = GetRequiredFieldErrorMessage(_isUserValid, "Użytkownik");
					break;
				case nameof(Password):
					Error = GetRequiredFieldErrorMessage(!string.IsNullOrWhiteSpace(Password), "Hasło"); ;
					break;
			}
			return Error;
		}
	}

	#endregion IDataErrorInfo Members
}