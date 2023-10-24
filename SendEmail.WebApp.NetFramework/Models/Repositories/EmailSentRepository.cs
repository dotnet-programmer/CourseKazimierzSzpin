using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendEmail.WebApp.NetFramework.Models.Domains;

namespace SendEmail.WebApp.NetFramework.Models.Repositories
{
	public class EmailSentRepository
	{
		internal void AddEmailSent(EmailSent emailSent)
		{
			using (AppDbContext context = new AppDbContext())
			{
				context.Emails.Add(emailSent);
				context.SaveChanges();
			}
		}

		internal List<EmailSent> GetEmails(string userId)
		{
			using (AppDbContext context = new AppDbContext())
			{
				return context.Emails
					.Where(x => x.UserId == userId)
					.ToList();
			}
		}
	}
}