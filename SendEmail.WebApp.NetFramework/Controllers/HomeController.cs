using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmailSender.NetFramework.Lib;
using Microsoft.AspNet.Identity;
using SendEmail.WebApp.NetFramework.Models.Domains;
using SendEmail.WebApp.NetFramework.Models.Repositories;

namespace SendEmail.WebApp.NetFramework.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly EmailSettingsRepository _emailSettingsRepository = new EmailSettingsRepository();
		private readonly EmailSentRepository _emailSentRepository = new EmailSentRepository();

		public ActionResult Index()
		{
			return (_emailSettingsRepository.GetEmailSettings(GetUserId()) == null) ? View("Settings", new EmailSettings { UserId = GetUserId() }) : View(new EmailSent());
		}

		public ActionResult Contacts()
		{
			ViewBag.Message = "Your Contacts page.";

			return View();
		}

		public ActionResult History()
		{
			ViewBag.Message = "Your History page.";

			return View(_emailSentRepository.GetEmails(GetUserId()));
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
			EmailSettings emailSettings = _emailSettingsRepository.GetEmailSettings(GetUserId()) ?? new EmailSettings { UserId = GetUserId() };
			return View(emailSettings);
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

		// error związany z async - trzeba oznaczyć stronę jako asynchroniczną
		// <%@ Page Async="true" %>
		[HttpPost]
		[ValidateAntiForgeryToken]
		//public async Task<ActionResult> SendEmail(EmailSent emailSent)
		public ActionResult SendEmail(EmailSent emailSent)
		{
			EmailParams emailParams = GetEmailParams();
			Email email = new Email(emailParams);

			emailSent.UserId = GetUserId();
			emailSent.DateSent = DateTime.Now;
			emailSent.SenderName = emailParams.SenderName;
			emailSent.SenderEmail = emailParams.SenderEmail;

			try
			{
				email.Send(emailSent.Subject, emailSent.Message, emailSent.RecipientEmail);
				_emailSentRepository.AddEmailSent(emailSent);
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = ex.Message;
				return View(false);
			}

			return View(true);
		}

		private string GetUserId() => User.Identity.GetUserId();

		private EmailSettings GetNewEmailSettings(string userId) => new EmailSettings { UserId = userId };

		private EmailParams GetEmailParams()
		{
			EmailSettings emailSettings = _emailSettingsRepository.GetEmailSettings(GetUserId());
			return new EmailParams 
			{
				HostSmtp = emailSettings.HostSmtp,
				Port = emailSettings.Port,
				EnableSsl = emailSettings.EnableSsl,
				SenderName = emailSettings.SenderName,
				SenderEmail = emailSettings.SenderEmail,
				SenderEmailPassword = emailSettings.SenderEmailPassword,
			};
		}
	}
}