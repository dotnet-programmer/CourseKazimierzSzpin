using System.Collections.Generic;
using System.Linq;
using SendEmail.NetFramework.WebApp.Models.Domains;

namespace SendEmail.NetFramework.WebApp.Models.Repositories
{
	public class ContactRepository
	{
		internal void AddNewContact(Contact newContact)
		{
			using (AppDbContext context = new AppDbContext())
			{
				context.Contacts.Add(newContact);
				context.SaveChanges();
			}
		}

		internal bool CheckIfContactExists(string recipientEmail, string userId)
		{
			using (AppDbContext context = new AppDbContext())
			{
				return context.Contacts.SingleOrDefault(x => x.Email == recipientEmail && x.UserId == userId) != null;
			}
		}

		internal void DeleteContact(int contactId, string userId)
		{
			using (AppDbContext context = new AppDbContext())
			{
				var contactToDelete = context.Contacts.Single(x => x.ContactId == contactId && x.UserId == userId);
				context.Contacts.Remove(contactToDelete);
				context.SaveChanges();
			}
		}

		internal List<Contact> GetContacts(string userId)
		{
			using (AppDbContext context = new AppDbContext())
			{
				return context.Contacts
					.Where(x => x.UserId == userId)
					.ToList();
			}
		}

		internal int GetContactCount(string userId)
		{
			using (AppDbContext context = new AppDbContext())
			{
				return context.Contacts
					.Where(x => x.UserId == userId)
					.Count();
			}
		}
	}
}