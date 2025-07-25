using SendEmail.WebApp.Core.Services;
using TextEncryption.Lib;

namespace SendEmail.WebApp.Persistence.Services;

public class EncryptionService(string key) : IEncryptionService
{
	private readonly StringCipher _stringCipher = new(key);

	public string Encrypt(string input)
		=> _stringCipher.Encrypt(input);

	public string Decrypt(string cipherText)
		=> _stringCipher.Decrypt(cipherText);
}