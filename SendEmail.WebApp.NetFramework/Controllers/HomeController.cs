using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SendEmail.WebApp.NetFramework.Models.Domains;
using SendEmail.WebApp.NetFramework.Models.Repositories;
using SendEmail.WebApp.NetFramework.Models.ViewModels;

namespace SendEmail.WebApp.NetFramework.Controllers
{
	public class HomeController : Controller
	{
		private readonly EmailSettingsRepository _emailSettingsRepository = new EmailSettingsRepository();

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Contacts()
		{
			ViewBag.Message = "Your Contacts page.";

			return View();
		}

		public ActionResult History()
		{
			ViewBag.Message = "Your History page.";

			return View();
		}

		//public ActionResult Settings(int emailSettingsId = 0)
		//{
		//	string userId = GetUserId();
		//	EmailSettings emailSettings = (emailSettingsId == 0) ? GetNewEmailSettings(userId) : _emailSettingsRepository.GetEmailSettings(userId);
		//	return View(emailSettings);

		//	//var userId = GetUserId();
		//	//EmailSettings emailSettings = (emailSettingsId == 0) ? GetNewEmailSettings(userId) : _emailSettingsRepository.GetEmailSettings(userId);
		//	//EmailSettingsViewModel emailSettingsVM = (emailSettingsId == 0) ? GetNewEmailSettings(userId) : _emailSettingsRepository.GetEmailSettings(userId);
		//	//return View(emailSettingsVM);
		//	//return View(_emailSettingsRepository.GetEmailSettings(GetUserId()));
		//}

		public ActionResult Settings()
		{
			return View(_emailSettingsRepository.GetEmailSettings(GetUserId()));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Settings(EmailSettings emailSettings)
		{
			var userId = GetUserId();
			emailSettings.UserId = userId;

			if (!ModelState.IsValid)
			{
				return View("Settings", emailSettings);
			}

			if (emailSettings.EmailSettingsId == 0)
			{
				_emailSettingsRepository.Add(emailSettings);
			}
			else
			{
				_emailSettingsRepository.Update(emailSettings);
			}
			return RedirectToAction("Index");
		}

		private string GetUserId() => User.Identity.GetUserId();

		private EmailSettings GetNewEmailSettings(string userId)
			=> new EmailSettings
			{
				UserId = userId,
			};

	}
}