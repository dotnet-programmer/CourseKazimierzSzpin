using System.Data.Entity;
using InvoiceManager.NetFramework.WebApp.Models.Domains;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InvoiceManager.NetFramework.WebApp.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoicePosition> InvoicePositions { get; set; }
		public DbSet<MethodOfPayment> MethodOfPayments { get; set; }
		public DbSet<Product> Products { get; set; }

		public static ApplicationDbContext Create() 
			=> new ApplicationDbContext();

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// INFO - zapobieganie kaskadowemu usuwaniu wspisów w bazie danych, czyli usunięcie użytkownika nie spowoduje usunięcia faktur ani klientów
			modelBuilder.Entity<ApplicationUser>()
				.HasMany(x => x.Invoices)
				.WithRequired(x => x.User)
				.HasForeignKey(x => x.UserId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(x => x.Clients)
				.WithRequired(x => x.User)
				.HasForeignKey(x => x.UserId)
				.WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
	}
}