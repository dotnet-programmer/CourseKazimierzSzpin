using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.WebApp.Persistence;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
	public DbSet<Address> Addresses { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<Invoice> Invoices { get; set; }
	public DbSet<InvoicePosition> InvoicePositions { get; set; }
	public DbSet<MethodOfPayment> MethodOfPayments { get; set; }
	public DbSet<Product> Products { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<ApplicationUser>()
			.HasMany(x => x.Invoices)
			.WithOne(x => x.User)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<ApplicationUser>()
			.HasMany(x => x.Clients)
			.WithOne(x => x.User)
			.OnDelete(DeleteBehavior.Restrict);

		base.OnModelCreating(builder);
	}
}