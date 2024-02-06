using System.ComponentModel.DataAnnotations;

namespace Advertisements.WebApp.Data.Core.Models.Domains;

public class Advertisement
{
	public int AdvertisementId { get; set; }

	[Required(ErrorMessage = "Pole Tytuł jest wymagane")]
	[Display(Name = "Tytuł")]
	[MaxLength(100)]
	public string Title { get; set; }

	[Required(ErrorMessage = "Pole Opis jest wymagane")]
	[Display(Name = "Opis")]
	public string Description { get; set; }

	[Required(ErrorMessage = "Pole Cena jest wymagane")]
	[Display(Name = "Cena")]
	public decimal Price { get; set; }

	[Required(ErrorMessage = "Pole Lokalizacja jest wymagane")]
	[Display(Name = "Lokalizacja")]
	[MaxLength(100)]
	public string Location { get; set; }

    [Required]
	public DateTime Added { get; set; }

	[Required(ErrorMessage = "Pole Data wygaśnięcia jest wymagane")]
	[Display(Name = "Data wygaśnięcia")]
	public DateTime Expiration { get; set; }

	public byte[] Picture { get; set; }
	public string PictureFormat { get; set; }

	[Required]
	public int CategoryId { get; set; }
	public Category Category { get; set; }

	[Required]
	public int SubcategoryId { get; set; }
	public Subcategory Subcategory { get; set; }

	[Required]
	public string UserId { get; set; }
	public ApplicationUser User { get; set; }
}