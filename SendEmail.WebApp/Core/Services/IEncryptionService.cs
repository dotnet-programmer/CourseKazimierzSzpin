namespace SendEmail.WebApp.Core.Services;

public interface IEncryptionService
{
	public string Encrypt(string input);
	public string Decrypt(string cipherText);
}