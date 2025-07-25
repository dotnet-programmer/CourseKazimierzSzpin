using Microsoft.AspNetCore.Mvc;
using SendEmail.WebApp.Core.Services;
using SendEmail.WebApp.Persistence.Extensions;

namespace SendEmail.WebApp.Controllers;

public class SettingsController(ISettingsService settingsService) : Controller
{
	public IActionResult Settings()
		=> View(settingsService.GetSettings(User.GetUserId()));

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Settings()
	{
		return View();
	}
}