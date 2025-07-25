using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Core.Services;

public interface ISettingsService
{
	Settings GetSettings(string userId);
	void AddSettings(Settings settings);
	void UpdateSettings(Settings settings);
}