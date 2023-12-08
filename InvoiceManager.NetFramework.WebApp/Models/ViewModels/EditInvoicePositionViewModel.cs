using System.Collections.Generic;
using InvoiceManager.NetFramework.WebApp.Models.Domains;

namespace InvoiceManager.NetFramework.WebApp.Models.ViewModels
{
	public class EditInvoicePositionViewModel
	{
		public InvoicePosition InvoicePosition { get; set; }
		public string Heading { get; set; }
		public List<Product> Products { get; set; }
	}
}