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
		=> (await context.Contacts.FirstOrDefaultAsync(contact => contact.Email == recipientEmail && contact.UserId == userId)) != null;

	public async Task<int> GetContactCountAsync(string userId)
		=> await context.Contacts.CountAsync(contact => contact.UserId == userId);

	public void RemoveContact(int id, string userId)
	{
		var contact = context.Contacts.FirstOrDefault(contact => contact.ContactId == id && contact.UserId == userId);
		if (contact != null)
		{
			context.Contacts.Remove(contact);
		}
		else
		{
			throw new KeyNotFoundException($"Contact with ID {id} for user {userId} not found.");
		}
	}
}