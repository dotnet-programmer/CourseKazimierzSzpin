using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Core.Repositories;

public interface ISettingsRepository
{
	Task<Settings?> GetSettingsAsync(string userId);
	void AddSettings(Settings settings);
	Task UpdateSettingsAsync(Settings settings);
}