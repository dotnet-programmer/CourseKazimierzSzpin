using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SendEmail.WebApp.NetFramework.Models.Domains;

namespace SendEmail.WebApp.NetFramework.Models
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

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}