using System.Data.Entity;
using InvoiceManager.WebApp.NetFramework.Models.Domains;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InvoiceManager.WebApp.NetFramework.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}