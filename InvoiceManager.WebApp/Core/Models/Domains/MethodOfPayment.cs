using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.Core.Models.Domains;

public class MethodOfPayment
{
	public int Id { get; set; }

	[Required]
	public string Name { get; set; } = default!;

	public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
}