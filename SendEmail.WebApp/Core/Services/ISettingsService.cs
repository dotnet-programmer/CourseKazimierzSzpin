using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Core.Services;

public interface ISettingsService
{
	Task<Settings> GetSettingsAsync(string userId);
	Task AddSettingsAsync(Settings settings);
	Task UpdateSettingsAsync(Settings settings);
}