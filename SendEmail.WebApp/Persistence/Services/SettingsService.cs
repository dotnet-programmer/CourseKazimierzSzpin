using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.Services;

namespace SendEmail.WebApp.Persistence.Services;

public class SettingsService(IUnitOfWork unitOfWork, IEncryptionService encryptionService) : ISettingsService
{
	public async Task<Settings> GetSettingsAsync(string userId)
	{
		var settings = await unitOfWork.SettingsRepository.GetSettingsAsync(userId);
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

	public async Task AddSettingsAsync(Settings settings)
	{
		settings.SenderEmailPassword = encryptionService.Encrypt(settings.SenderEmailPassword);
		unitOfWork.SettingsRepository.AddSettings(settings);
		await unitOfWork.CompleteAsync();
	}

	public async Task UpdateSettingsAsync(Settings settings)
	{
		settings.SenderEmailPassword = encryptionService.Encrypt(settings.SenderEmailPassword);
		await unitOfWork.SettingsRepository.UpdateSettingsAsync(settings);
		await unitOfWork.CompleteAsync();
	}
}