using InvoiceManager.WebApp.Core.Models.Domains;

namespace InvoiceManager.WebApp.Core.ViewModels;

public class EditInvoiceViewModel
{
	public Invoice? Invoice { get; set; }
	public List<Client>? Clients { get; set; }
	public List<MethodOfPayment>? MethodOfPayments { get; set; }
	public string? Heading { get; set; }
}