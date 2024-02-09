using System.ComponentModel.DataAnnotations;
using Advertisements.WebApp.Data.Core.Models.Domains;

namespace Advertisements.WebApp.Data.Core.ViewModels;

public class AdvertisementViewModel
{
	public string Heading { get; set; }
	public Advertisement Advertisement { get; set; }
	public IEnumerable<Category> Categories { get; set; }

	[Display(Name = "Zdjęcie")]
	public IFormFile Picture { get; set; }
}
