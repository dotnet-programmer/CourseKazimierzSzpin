using System.Configuration;
using TextEncryption.Lib;

namespace HumanResources.Homework.WpfApp.Models;

internal static class StringCipherHelper
{
	private const string NOT_ENCRYPTED_PASSWORD_PREFIX = "encrypt:";
	private static readonly StringCipher _stringCipher = new("4838731F-FC44-40B9-9952-EE5CCB6C198E");

	public static void EncryptStringFromConfigAndSave(string appSettingName)
	{
		var stringToEncrypt = ConfigurationManager.AppSettings[appSettingName];
		if (stringToEncrypt.StartsWith(NOT_ENCRYPTED_PASSWORD_PREFIX))
		{
			stringToEncrypt = _stringCipher.Encrypt(stringToEncrypt.Replace(NOT_ENCRYPTED_PASSWORD_PREFIX, string.Empty));
			var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			configFile.AppSettings.Settings[appSettingName].Value = stringToEncrypt;
			configFile.Save();
			ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
		}
	}

	public static string EncryptString(string stringToEncrypt) => _stringCipher.Encrypt(stringToEncrypt);

	public static string DecryptString(string stringToDecrypt) => _stringCipher.Decrypt(stringToDecrypt);

	public static string DecryptStringFromConfig(string key) => _stringCipher.Decrypt(ConfigurationManager.AppSettings[key]);
}