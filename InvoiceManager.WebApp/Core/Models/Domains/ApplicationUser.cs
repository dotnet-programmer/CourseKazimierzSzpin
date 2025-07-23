using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace InvoiceManager.WebApp.Core.Models.Domains;

public class ApplicationUser : IdentityUser
{
	[Required]
	public string Name { get; set; }
	
	public int AddressId { get; set; }

	public Address? Address { get; set; }
	public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
	public ICollection<Client> Clients { get; set; } = new Collection<Client>();
}