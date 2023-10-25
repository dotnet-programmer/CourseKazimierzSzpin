using System.Linq;
using SendEmail.WebApp.NetFramework.Models.Domains;
using TextEncryption.NetFramework.Lib;

namespace SendEmail.WebApp.NetFramework.Models.Repositories
{
	public class EmailSettingsRepository
	{
		private readonly StringCipher _stringCipher = new StringCipher("4838731F-FC44-40B9-9952-EE5CCB6C198E");

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
				EmailSettings emailSettings = context.EmailSettings.SingleOrDefault(x => x.UserId == userId);
				if (emailSettings != null && !string.IsNullOrWhiteSpace(emailSettings.SenderEmailPassword))
				{
					emailSettings.SenderEmailPassword = _stringCipher.Decrypt(emailSettings.SenderEmailPassword);
				}
				return emailSettings;
			}
		}

		internal void Add(EmailSettings emailSettings)
		{
			using (AppDbContext context = new AppDbContext())
			{
				emailSettings.SenderEmailPassword = _stringCipher.Encrypt(emailSettings.SenderEmailPassword);
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
				emailSettingsToUpdate.SenderEmailPassword = _stringCipher.Encrypt(emailSettings.SenderEmailPassword);
				emailSettingsToUpdate.HostSmtp = emailSettings.HostSmtp;
				emailSettingsToUpdate.EnableSsl = emailSettings.EnableSsl;
				emailSettingsToUpdate.Port = emailSettings.Port;

				context.SaveChanges();
			}
		}
	}
}