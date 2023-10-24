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

        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailSettings> EmailSettings { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}