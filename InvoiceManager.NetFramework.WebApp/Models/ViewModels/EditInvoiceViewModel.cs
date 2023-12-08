using System.Collections.Generic;
using InvoiceManager.NetFramework.WebApp.Models.Domains;

namespace InvoiceManager.NetFramework.WebApp.Models.ViewModels
{
	public class EditInvoiceViewModel
	{
		public Invoice Invoice { get; set; }
		public List<Client> Clients { get; set; }
		public List<MethodOfPayment> MethodOfPayments { get; set; }
		public string Heading { get; set; }
	}
}