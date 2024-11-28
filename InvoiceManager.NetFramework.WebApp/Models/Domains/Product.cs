using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.NetFramework.WebApp.Models.Domains
{
	public class Product
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public decimal Value { get; set; }

		public ICollection<InvoicePosition> InvoicePositions { get; set; } = new Collection<InvoicePosition>();
	}
}