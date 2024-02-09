using Advertisements.WebApp.Data.Core.Models;
using Advertisements.WebApp.Data.Core.Models.Domains;

namespace Advertisements.WebApp.Data.Core.ViewModels;

public class AdvertisementsViewModel
{
	//public IEnumerable<Advertisement> Advertisements { get; set; }
	public IEnumerable<Category> Categories { get; set; }
	//public IEnumerable<Subcategory> Subcategories { get; set; }
	public FilterAdvertisements FilterAdvertisements { get; set; }
	public IEnumerable<AdvertisementsTableItem> Advertisements { get; set; }

}