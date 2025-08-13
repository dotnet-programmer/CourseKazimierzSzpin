using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Core.Repositories;

public interface IContactRepository
{
	void AddNewContact(Contact contact);
	Task<bool> CheckIfContactExistsAsync(string recipientEmail, string userId);
	Task<int> GetContactCountAsync(string userId);
}