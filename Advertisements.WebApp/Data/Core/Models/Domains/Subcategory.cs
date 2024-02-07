using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Advertisements.WebApp.Data.Core.Models.Domains;

public class Subcategory
{
	public int SubcategoryId { get; set; }

	[Required(ErrorMessage = "Pole Nazwa jest wymagane")]
	[Display(Name = "Nazwa")]
	[MaxLength(50)]
	public string Name { get; set; }

	[Required]
	public int CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Advertisement> Advertisements { get; set; } = new Collection<Advertisement>();
}