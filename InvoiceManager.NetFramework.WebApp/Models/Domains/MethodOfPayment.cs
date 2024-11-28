using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.NetFramework.WebApp.Models.Domains
{
	public class MethodOfPayment
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
	}
}