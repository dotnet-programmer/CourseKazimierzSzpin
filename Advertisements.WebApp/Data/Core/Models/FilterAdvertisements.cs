using System.ComponentModel.DataAnnotations;

namespace Advertisements.WebApp.Data.Core.Models;

public class FilterAdvertisements
{
	[Display(Name = "Tytuł")]
	public string Title { get; set; } = string.Empty;

	[Display(Name = "Kategoria")]
	public int CategoryId { get; set; }

	//[Display(Name = "Podkategoria")]
	//public int SubcategoryId { get; set; }

	[Display(Name = "Cena od")]
	public decimal MinPrice { get; set; }

	[Display(Name = "Cena Do")]
	public decimal MaxPrice { get; set; }

	[Display(Name = "Archiwalne")]
	public bool IsArchival { get; set; }
}