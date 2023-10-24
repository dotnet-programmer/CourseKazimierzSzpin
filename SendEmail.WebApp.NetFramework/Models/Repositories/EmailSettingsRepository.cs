using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SendEmail.WebApp.NetFramework.Models.Domains;

namespace SendEmail.WebApp.NetFramework.Models.Repositories
{
	public class EmailSettingsRepository
	{
		//internal List<EmailSettings> GetEmailSettings(string userId)
		//{
		//	using (AppDbContext context = new AppDbContext())
		//	{
		//		return context.EmailSettings.Where(x => x.UserId == userId).ToList();
		//	}
		//}

		internal EmailSettings GetEmailSettings(string userId)
		{
			using (AppDbContext context = new AppDbContext())
			{
				return context.EmailSettings.SingleOrDefault(x => x.UserId == userId);
			}
		}

		internal void Add(EmailSettings emailSettings)
		{
			using (AppDbContext context = new AppDbContext())
			{
				context.EmailSettings.Add(emailSettings);
				context.SaveChanges();
			}
		}

		internal void Update(EmailSettings emailSettings)
		{
			using (AppDbContext context = new AppDbContext())
			{
				EmailSettings emailSettingsToUpdate = context.EmailSettings
					.Single(x => x.EmailSettingsId == emailSettings.EmailSettingsId && x.UserId == emailSettings.UserId);

				emailSettingsToUpdate.SenderName = emailSettings.SenderName;
				emailSettingsToUpdate.SenderEmail = emailSettings.SenderEmail;
				// TODO - dodać szyfrowanie hasła
				emailSettingsToUpdate.SenderEmailPassword = emailSettings.SenderEmailPassword;
				emailSettingsToUpdate.HostSmtp = emailSettings.HostSmtp;
				emailSettingsToUpdate.EnableSsl = emailSettings.EnableSsl;
				emailSettingsToUpdate.Port = emailSettings.Port;

				context.SaveChanges();
			}
		}
	}
}