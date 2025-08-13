using Microsoft.EntityFrameworkCore;
using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.Repositories;

namespace SendEmail.WebApp.Persistence.Repositories;

public class EmailRepository(IApplicationDbContext context) : IEmailRepository
{
	public void AddEmailSent(EmailSent newEmail)
		=> context.Emails.Add(newEmail);

	public async Task<int> GetEmailCountAsync(string userId)
		=> await context.Emails.CountAsync(email => email.UserId == userId);

	public async Task<List<EmailSent>> GetEmailsAsync()
		=> await context.Emails
			.AsNoTracking()
			.OrderByDescending(email => email.DateSent)
			.ToListAsync();

	public void RemoveEmail(int emailId)
		=> context.Emails.Remove(new EmailSent { EmailSentId = emailId });
}