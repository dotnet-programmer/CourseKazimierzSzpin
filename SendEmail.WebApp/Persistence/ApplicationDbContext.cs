using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Models.Domains;

namespace SendEmail.WebApp.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
	public DbSet<EmailSent> Emails { get; set; }
	public DbSet<Settings> Settings { get; set; }
	public DbSet<Contact> Contacts { get; set; }
}