using Microsoft.AspNetCore.Mvc;
using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.Services;
using SendEmail.WebApp.Persistence.Extensions;

namespace SendEmail.WebApp.Controllers;

public class SettingsController(ISettingsService settingsService) : Controller
{
	public IActionResult Settings()
		=> View(settingsService.GetSettings(User.GetUserId()));

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Settings(Settings settings)
	{
		settings.UserId = User.GetUserId();

		if (!ModelState.IsValid)
		{
			return View(settings);
		}

		if (settings.SettingsId == 0)
		{
			settingsService.AddSettings(settings);
		}
		else
		{
			settingsService.UpdateSettings(settings);
		}

		return RedirectToAction("Index", "Home");
	}
}