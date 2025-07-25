using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.Repositories;

namespace SendEmail.WebApp.Persistence.Repositories;

public class SettingsRepository(IApplicationDbContext context) : ISettingsRepository
{
	public Settings? GetSettings(string userId)
		=> context.Settings.SingleOrDefault(x => x.UserId == userId);

	public void AddSettings(Settings settings)
		=> context.Settings.Add(settings);

	public void UpdateSettings(Settings settings)
	{
		Settings settingsToUpdate = context.Settings.Single(x => x.SettingsId == settings.SettingsId && x.UserId == settings.UserId);
		settingsToUpdate.SenderName = settings.SenderName;
		settingsToUpdate.SenderEmail = settings.SenderEmail;
		settingsToUpdate.SenderEmailPassword = settings.SenderEmailPassword;
		settingsToUpdate.HostSmtp = settings.HostSmtp;
		settingsToUpdate.EnableSsl = settings.EnableSsl;
		settingsToUpdate.Port = settings.Port;
	}
}