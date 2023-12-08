using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmailSender.NetFramework.Lib;
using Microsoft.AspNet.Identity;
using SendEmail.NetFramework.WebApp.Models.Domains;
using SendEmail.NetFramework.WebApp.Models.Repositories;

namespace SendEmail.NetFramework.WebApp.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly EmailSettingsRepository _emailSettingsRepository = new EmailSettingsRepository();
		private readonly EmailSentRepository _emailSentRepository = new EmailSentRepository();
		private readonly ContactRepository _contactRepository = new ContactRepository();

		public ActionResult Index() => (_emailSettingsRepository.GetEmailSettings(GetUserId()) == null) ?
				View("Settings", new EmailSettings { UserId = GetUserId() }) :
				View(new EmailSent { UserId = GetUserId() });

		[HttpPost]
		public ActionResult Index(string recipientEmail, string subject = "", string message = "") => (_emailSettingsRepository.GetEmailSettings(GetUserId()) == null) ?
				View("Settings", new EmailSettings { UserId = GetUserId() }) :
				View(new EmailSent { UserId = GetUserId(), RecipientEmail = recipientEmail, Subject = subject, Message = message });

		public ActionResult Contacts() => View(_contactRepository.GetContacts(GetUserId()));

		public ActionResult History() => View(_emailSentRepository.GetEmails(GetUserId()));

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

			// wzorzec PRG - Post-Redirect-Get - po wysłaniu akcji Post, akcja robi redirect do innej akcji, która jest Get
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SendEmail(EmailSent emailSent)
		{
			var userId = GetUserId();
			EmailParams emailParams = GetEmailParams();
			Email email = new Email(emailParams);

			emailSent.UserId = userId;
			emailSent.DateSent = DateTime.Now;
			emailSent.SenderName = emailParams.SenderName;
			emailSent.SenderEmail = emailParams.SenderEmail;
			if (emailSent.Message == null)
			{
				emailSent.Message = "";
			}

			if (!ModelState.IsValid)
			{
				return View("Index", emailSent);
			}

			try
			{
				await email.Send(emailSent.Subject, emailSent.Message, emailSent.RecipientEmail);
				_emailSentRepository.AddEmailSent(emailSent);
				if (!_contactRepository.CheckIfContactExists(emailSent.RecipientEmail, userId))
				{
					_contactRepository.AddNewContact(new Contact { Email = emailSent.RecipientEmail, UserId = userId });
				}
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = ex.Message;
				return View(false);
			}

			return View(true);
		}

		[HttpPost]
		public ActionResult DeleteContact(int contactId)
		{
			try
			{
				_contactRepository.DeleteContact(contactId, GetUserId());
			}
			catch (Exception ex)
			{
				return Json(new { Success = false, ex.Message });
			}
			return Json(new { Success = true });
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