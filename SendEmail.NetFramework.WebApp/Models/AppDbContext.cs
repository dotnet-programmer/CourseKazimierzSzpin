using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SendEmail.NetFramework.WebApp.Models.Domains;

namespace SendEmail.NetFramework.WebApp.Models
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<EmailSent> Emails { get; set; }
		public DbSet<EmailSettings> EmailSettings { get; set; }
		public DbSet<Contact> Contacts { get; set; }

		public static AppDbContext Create() => new AppDbContext();
	}
}