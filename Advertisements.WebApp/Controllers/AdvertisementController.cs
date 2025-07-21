using Advertisements.WebApp.Data.Core.Models;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Services;
using Advertisements.WebApp.Data.Core.ViewModels;
using Advertisements.WebApp.Data.Persistence.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisements.WebApp.Controllers;

[Authorize]
public class AdvertisementController(IAdvertisementService advertisementService, ICategoryService categoryService) : Controller
{
	[AllowAnonymous]
	public IActionResult Advertisements()
	{
		var advertisements = advertisementService.GetAdvertisements(new());

		List<AdvertisementsTableItem> advertisementsTableItems = [];
		foreach (var item in advertisements)
		{
			advertisementsTableItems.Add(new AdvertisementsTableItem { Advertisement = item, Picture = Convert.ToBase64String(item.Picture) });
		}

		AdvertisementsViewModel vm = new()
		{
			Advertisements = advertisementsTableItems,
			Categories = categoryService.GetCategories(),
			FilterAdvertisements = new()
		};

		return View(vm);
	}

	[HttpPost]
	public IActionResult Advertisements(AdvertisementsViewModel advertisementsViewModel)
	{
		var advertisements = advertisementService.GetAdvertisements(new()
		{
			Title = advertisementsViewModel.FilterAdvertisements.Title,
			CategoryId = advertisementsViewModel.FilterAdvertisements.CategoryId,
			MinPrice = advertisementsViewModel.FilterAdvertisements.MinPrice,
			MaxPrice = advertisementsViewModel.FilterAdvertisements.MaxPrice,
			IsArchival = advertisementsViewModel.FilterAdvertisements.IsArchival,
		});

		List<AdvertisementsTableItem> advertisementsTableItems = [];
		foreach (var item in advertisements)
		{
			advertisementsTableItems.Add(new AdvertisementsTableItem { Advertisement = item, Picture = Convert.ToBase64String(item.Picture) });
		}

		return PartialView("_AdvertisementsTable", advertisementsTableItems);
	}

	public IActionResult Advertisement(int advertisementId = 0)
	{
		var userId = User.GetUserId();

		Advertisement advertisement = advertisementId == 0 ?
			new Advertisement { AdvertisementId = advertisementId, UserId = userId, Added = DateTime.Today } :
			advertisementService.GetAdvertisement(advertisementId, userId);

		return View(GetAdvertisementViewModel(advertisement));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Advertisement(AdvertisementViewModel advertisementViewModel)
	{
		ArgumentNullException.ThrowIfNull(advertisementViewModel);

		advertisementViewModel.Advertisement.UserId = User.GetUserId();

		// TODO - dodać wykluczenia żeby puszczało dalej
		//if (!ModelState.IsValid)
		//{
		//	//AdvertisementViewModel vm = GetAdvertisementViewModel(advertisementViewModel.Advertisement);
		//	AdvertisementViewModel vm = GetAdvertisementViewModel(advertisementViewModel);
		//	return View("Advertisement", vm);
		//}

		// The picture format is not passed directly to the action method but can be retrieved from the IFormFile parameter
		advertisementViewModel.Advertisement.PictureFormat = advertisementViewModel.Picture.ContentType;

		// The contents of the file are stored as a stream in the IFormFile object. For storage in the database, this stream has to be converted into a byte array.
		// Fortunately, this can be easily done by using a MemoryStream.
		MemoryStream memoryStream = new();
		advertisementViewModel.Picture.CopyTo(memoryStream);
		advertisementViewModel.Advertisement.Picture = memoryStream.ToArray();

		// The user object is now completed filled, so it can be stored in the database in the normal Entity Framework way.
		if (advertisementViewModel.Advertisement.AdvertisementId == 0)
		{
			advertisementService.AddAdvertisement(advertisementViewModel.Advertisement);
		}
		else
		{
			advertisementService.UpdateAdvertisement(advertisementViewModel.Advertisement);
		}

		return RedirectToAction("Advertisements");
	}

	private AdvertisementViewModel GetAdvertisementViewModel(Advertisement advertisement)
		=> new()
		{
			Advertisement = advertisement,
			Heading = advertisement.AdvertisementId == 0 ? "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia",
			Categories = categoryService.GetCategories(),
		};

	private AdvertisementViewModel GetAdvertisementViewModel(AdvertisementViewModel advertisementViewModel)
		=> new()
		{
			Advertisement = advertisementViewModel.Advertisement,
			Heading = advertisementViewModel.Advertisement.AdvertisementId == 0 ? "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia",
			Categories = categoryService.GetCategories(),
			Picture = advertisementViewModel.Picture
		};
}