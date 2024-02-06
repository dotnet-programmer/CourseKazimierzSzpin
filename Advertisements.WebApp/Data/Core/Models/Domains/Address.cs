using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.WebApp.Data.Core.Models.Domains;

public class Address
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Pole Miejscowość jest wymagane")]
	[Display(Name = "Miejscowość")]
	[MaxLength(100)]
	public string City { get; set; }

	[Required(ErrorMessage = "Pole Ulica jest wymagane")]
	[Display(Name = "Ulica")]
	[MaxLength(150)]
	public string Street { get; set; }

	[Required(ErrorMessage = "Pole Numer jest wymagane")]
	[Display(Name = "Numer")]
	[MaxLength(20)]
	public string Number { get; set; }

	[Required(ErrorMessage = "Pole Kod pocztowy jest wymagane")]
	[Display(Name = "Kod pocztowy")]
	[MaxLength(6)]
	public string PostalCode { get; set; }

	public ICollection<ApplicationUser> Users { get; set; } = new Collection<ApplicationUser>();
}