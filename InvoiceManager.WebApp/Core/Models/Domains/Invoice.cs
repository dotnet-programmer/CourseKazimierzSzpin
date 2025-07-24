using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.Core.Models.Domains;

public class Invoice
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Pole Tytuł jest wymagane")]
	[Display(Name = "Tytuł")]
	public string Title { get; set; } = default!;

	[Display(Name = "Wartość")]
	public decimal Value { get; set; }

	[Display(Name = "Termin płatności")]
	public DateTime PaymentDate { get; set; }

	[Display(Name = "Data utworzenia")]
	public DateTime CreatedDate { get; set; }

	[Display(Name = "Uwagi")]
	public string? Comments { get; set; }

	[Display(Name = "Sposób płatności")]
	public int MethodOfPaymentId { get; set; }

	[Display(Name = "Klient")]
	public int ClientId { get; set; }

	[Required]
	public string UserId { get; set; } = default!;

	public MethodOfPayment? MethodOfPayment { get; set; }
	public Client? Client { get; set; }
	public ApplicationUser? User { get; set; }
	public ICollection<InvoicePosition> InvoicePositions { get; set; } = new Collection<InvoicePosition>();
}