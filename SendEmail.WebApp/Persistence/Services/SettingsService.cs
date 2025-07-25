using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.Services;

namespace SendEmail.WebApp.Persistence.Services;

public class SettingsService(IUnitOfWork unitOfWork, IEncryptionService encryptionService) : ISettingsService
{
	public Settings GetSettings(string userId)
	{
		var settings = unitOfWork.SettingsRepository.GetSettings(userId);
		if (settings != null && !string.IsNullOrWhiteSpace(settings.SenderEmailPassword))
		{
			settings.SenderEmailPassword = encryptionService.Decrypt(settings.SenderEmailPassword);
		}
		else
		{
			settings = new() { UserId = userId };
		}
		return settings;
	}

	public void AddSettings(Settings settings)
	{
		settings.SenderEmailPassword = encryptionService.Encrypt(settings.SenderEmailPassword);
		unitOfWork.SettingsRepository.AddSettings(settings);
		unitOfWork.Complete();
	}

	public void UpdateSettings(Settings settings)
	{
		settings.SenderEmailPassword = encryptionService.Encrypt(settings.SenderEmailPassword);
		unitOfWork.SettingsRepository.UpdateSettings(settings);
		unitOfWork.Complete();
	}
}