using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.NetFramework.WebApp.Models.Domains
{
	public class Address
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Ulica")]
		public string Street { get; set; }

		[Required]
		[Display(Name = "Numer")]
		public string Number { get; set; }

		[Required]
		[Display(Name = "Miejscowość")]
		public string City { get; set; }

		[Required]
		[Display(Name = "Kod pocztowy")]
		public string PostalCode { get; set; }

		public ICollection<Client> Clients { get; set; } = new Collection<Client>();
		public ICollection<ApplicationUser> Users { get; set; } = new Collection<ApplicationUser>();
	}
}