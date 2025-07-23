using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.Core.Models.Domains;

public class Client
{
	public int Id { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public string Email { get; set; }

	public int AddressId { get; set; }

	// każdy użytkownik ma swoich klientów
	[Required]
	[ForeignKey(nameof(User))]
	public string UserId { get; set; }

	public Address? Address { get; set; }
	public ApplicationUser? User { get; set; }
	public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
}