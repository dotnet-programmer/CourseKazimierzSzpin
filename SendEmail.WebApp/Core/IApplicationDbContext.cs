using Microsoft.EntityFrameworkCore;
using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Core;

public interface IApplicationDbContext
{
	DbSet<Contact> Contacts { get; set; }
	DbSet<EmailSent> Emails { get; set; }
	DbSet<Settings> Settings { get; set; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}