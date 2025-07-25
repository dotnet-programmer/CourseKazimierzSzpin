using Microsoft.EntityFrameworkCore;
using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Core;

public interface IApplicationDbContext
{
	DbSet<EmailSent> Emails { get; set; }
	DbSet<Settings> Settings { get; set; }
	DbSet<Contact> Contacts { get; set; }
	int SaveChanges();
}