using Microsoft.EntityFrameworkCore;
using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Models.Domains;
using SendEmail.WebApp.Core.Repositories;

namespace SendEmail.WebApp.Persistence.Repositories;

public class ContactRepository(IApplicationDbContext context) : IContactRepository
{
	public void AddNewContact(Contact contact)
		=> context.Contacts.Add(contact);

	public async Task<bool> CheckIfContactExistsAsync(string recipientEmail, string userId)
		=> (await context.Contacts.FirstOrDefaultAsync(x => x.Email == recipientEmail && x.UserId == userId)) != null;

	public async Task<int> GetContactCountAsync(string userId)
		=> await context.Contacts.CountAsync(contact => contact.UserId == userId);
}