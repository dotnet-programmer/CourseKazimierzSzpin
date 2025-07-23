using InvoiceManager.WebApp.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.WebApp.Core;

public interface IApplicationDbContext
{
	public DbSet<Address> Addresses { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<Invoice> Invoices { get; set; }
	public DbSet<InvoicePosition> InvoicePositions { get; set; }
	public DbSet<MethodOfPayment> MethodOfPayments { get; set; }
	public DbSet<Product> Products { get; set; }
	int SaveChanges();
}