using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Core.Repositories;

public interface IEmailRepository
{
	void AddEmailSent(EmailSent newEmail);
	Task<int> GetEmailCountAsync(string userId);
	Task<List<EmailSent>> GetEmailsAsync();
	void RemoveEmail(int emailId);
}