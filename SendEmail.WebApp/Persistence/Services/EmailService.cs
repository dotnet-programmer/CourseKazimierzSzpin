using EmailSender.Lib;
using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.Services;
using SendEmail.WebApp.Core.ViewModels;

namespace SendEmail.WebApp.Persistence.Services;

public class EmailService(IUnitOfWork unitOfWork, ISettingsService settingsService) : IEmailService
{
	public async Task<int> GetEmailCountAsync(string userId)
	{
		if (string.IsNullOrWhiteSpace(userId))
		{
			throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
		}
		return await unitOfWork.EmailRepository.GetEmailCountAsync(userId);
	}

	public async Task<List<EmailSent>> GetEmailsAsync()
		=> await unitOfWork.EmailRepository.GetEmailsAsync();

	public async Task RemoveEmailAsync(int emailId, string userId)
	{
		if (emailId <= 0)
		{
			throw new ArgumentException("Email ID must be greater than zero.", nameof(emailId));
		}
		if (string.IsNullOrWhiteSpace(userId))
		{
			throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
		}
		unitOfWork.EmailRepository.RemoveEmail(emailId);
		await unitOfWork.CompleteAsync();
	}

	public async Task SendEmailAsync(string userId, NewEmailViewModel newEmailVM)
	{
		EmailParams emailParams = GetEmailParams(await settingsService.GetSettingsAsync(userId));

		EmailSent newEmail = new()
		{
			UserId = userId,
			DateSent = DateTime.Now,
			SenderName = emailParams.SenderName,
			SenderEmail = emailParams.SenderEmail,
			RecipientEmail = newEmailVM.RecipientEmail,
			Subject = newEmailVM.Subject,
			Message = newEmailVM.Message ?? string.Empty,
		};

		Email email = new(emailParams);

		await email.Send(newEmail.Subject, newEmail.Message, newEmail.RecipientEmail);

		unitOfWork.EmailRepository.AddEmailSent(newEmail);

		if (!(await unitOfWork.ContactRepository.CheckIfContactExistsAsync(newEmail.RecipientEmail, userId)))
		{
			unitOfWork.ContactRepository.AddNewContact(new Contact { Email = newEmail.RecipientEmail, UserId = userId });
		}

		await unitOfWork.CompleteAsync();
	}

	private EmailParams GetEmailParams(Settings settings)
		=> new()
		{
			HostSmtp = settings.HostSmtp,
			Port = settings.Port,
			EnableSsl = settings.EnableSsl,
			SenderName = settings.SenderName,
			SenderEmail = settings.SenderEmail,
			SenderEmailPassword = settings.SenderEmailPassword,
		};
}