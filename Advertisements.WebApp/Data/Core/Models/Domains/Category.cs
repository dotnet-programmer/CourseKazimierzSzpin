using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.WebApp.Data.Core.Models.Domains;

public class Category
{
	public int CategoryId { get; set; }

	[Required(ErrorMessage = "Pole Nazwa jest wymagane")]
	[Display(Name = "Nazwa")]
	[MaxLength(50)]
	public string Name { get; set; }

	public ICollection<Advertisement> Advertisements { get; set; } = new Collection<Advertisement>();
	//public ICollection<Subcategory> Subcategories { get; set; } = new Collection<Subcategory>();
}