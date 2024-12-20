﻿using System.Collections.Generic;
using System.Linq;
using SendEmail.NetFramework.WebApp.Models.Domains;

namespace SendEmail.NetFramework.WebApp.Models.Repositories
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

		internal int GetEmailCount(string userId)
		{
			using (AppDbContext context = new AppDbContext())
			{
				return context.Emails
					.Where(x => x.UserId == userId)
					.Count();
			}
		}
	}
}