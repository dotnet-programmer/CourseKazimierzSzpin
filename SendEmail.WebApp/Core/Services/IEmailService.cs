using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.ViewModels;

namespace SendEmail.WebApp.Core.Services;

public interface IEmailService
{
	Task<int> GetEmailCountAsync(string userId);
	Task<List<EmailSent>> GetEmailsAsync();
	Task RemoveEmailAsync(int emailId, string userId);
	Task SendEmailAsync(string userId, NewEmailViewModel newEmail);
}