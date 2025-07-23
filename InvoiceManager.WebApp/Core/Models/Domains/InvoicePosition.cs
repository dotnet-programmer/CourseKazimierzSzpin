using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.Core.Models.Domains;

public class InvoicePosition
{
	public int Id { get; set; }
	public int Lp { get; set; }

	[Display(Name = "Wartość")]
	public decimal Value { get; set; }

	[Display(Name = "Ilość")]
	public int Quantity { get; set; }

	public int InvoiceId { get; set; }

	[Display(Name = "Produkt")]
	public int ProductId { get; set; }

	public Invoice? Invoice { get; set; }
	public Product? Product { get; set; }
}