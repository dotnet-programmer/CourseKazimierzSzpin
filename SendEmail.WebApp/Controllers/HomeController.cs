using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.Services;
using SendEmail.WebApp.Core.ViewModels;
using SendEmail.WebApp.Persistence.Extensions;

namespace SendEmail.WebApp.Controllers;

[Authorize]
public class HomeController(IContactService contactService, IEmailService emailService, ISettingsService settingsService) : Controller
{
	[AllowAnonymous]
	public async Task<IActionResult> Index()
		=> View(await GetIndexVMAsync());

	public async Task<IActionResult> NewEmail()
		=> View(await GetNewEmailVMAsync());

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> NewEmail(NewEmailViewModel newEmail)
	{
		if (!ModelState.IsValid)
		{
			return View(await GetNewEmailVMAsync(newEmail));
		}

		try
		{
			await emailService.SendEmailAsync(User.GetUserId(), newEmail);
		}
		catch (Exception ex)
		{
			ViewBag.ErrorMessage = ex.Message;
			return View("SendEmail", false);
		}

		return View("SendEmail", true);
	}

	public IActionResult Contacts()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> DeleteContact(int id)
	{
		try
		{
			await contactService.RemoveContactAsync(id, User.GetUserId());
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}

	public async Task<IActionResult> History()
		=> View(await emailService.GetEmailsAsync());

	[HttpPost]
	public async Task<IActionResult> DeleteEmail(int id)
	{
		try
		{
			await emailService.RemoveEmailAsync(id, User.GetUserId());
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}

	public async Task<IActionResult> Settings()
		=> View(await settingsService.GetSettingsAsync(User.GetUserId()));

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Settings(Settings settings)
	{
		settings.UserId = User.GetUserId();

		if (!ModelState.IsValid)
		{
			return View(settings);
		}

		if (settings.SettingsId == 0)
		{
			await settingsService.AddSettingsAsync(settings);
		}
		else
		{
			await settingsService.UpdateSettingsAsync(settings);
		}

		return RedirectToAction("Index", "Home");
	}

	[AllowAnonymous]
	public IActionResult Privacy()
		=> View();

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
		=> View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

	private async Task<IndexViewModel?> GetIndexVMAsync()
	{
		if (!User.Identity?.IsAuthenticated ?? true)
		{
			return null;
		}

		var userId = User.GetUserId();
		return new IndexViewModel
		{
			Heading = "Statystyki u¿ytkownika",
			NumberOfContacts = await contactService.GetContactCountAsync(userId),
			NumberOfEmails = await emailService.GetEmailCountAsync(userId)
		};
	}

	private async Task<NewEmailViewModel> GetNewEmailVMAsync()
		=> new()
		{
			SettingsId = await GetSettingsId(),
		};

	private async Task<NewEmailViewModel> GetNewEmailVMAsync(NewEmailViewModel newEmailViewModel)
		=> new()
		{
			SettingsId = await GetSettingsId(),
			RecipientEmail = newEmailViewModel.RecipientEmail,
			Subject = newEmailViewModel.Subject,
			Message = newEmailViewModel.Message,
		};

	private async Task<int> GetSettingsId()
		=> (await settingsService.GetSettingsAsync(User.GetUserId())).SettingsId;
}