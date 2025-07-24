using InvoiceManager.WebApp.Core.Models.Domains;

namespace InvoiceManager.WebApp.Core.ViewModels;

public class EditInvoicePositionViewModel
{
	public InvoicePosition InvoicePosition { get; set; } = default!;
	public string? Heading { get; set; }
	public List<Product>? Products { get; set; }
}