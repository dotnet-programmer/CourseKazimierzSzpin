using System.Diagnostics;
using Advertisements.WebApp.Data.Core.Services;
using Advertisements.WebApp.Data.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisements.WebApp.Controllers;

[Authorize]
public class AdvertisementController : Controller
{
	private readonly IAdvertisementService _advertisementService;
	private readonly ICategoryService _categoryService;

	// TODO - lista zdań:
	// funkcja dodawania ogłoszeń
	// zarządzanie ogłoszeniami przez usera
	// zarządzanie kategoriami dla admina
	// dodać użytkownikom adres przy rejestracji

	public AdvertisementController(IAdvertisementService advertisementService, ICategoryService categoryService)
	{
		_advertisementService = advertisementService;
		_categoryService = categoryService;
	}

	[AllowAnonymous]
	public IActionResult Advertisements()
	{
		AdvertisementsViewModel vm = new()
		{
			Advertisements = _advertisementService.GetAdvertisements(new()),
			Categories = _categoryService.GetCategories(),
			FilterAdvertisements = new()
		};

		return View(vm);
	}
	
	public IActionResult Advertisement()
	{
		return View();
	}

	public IActionResult Manage()
	{
		return View();
	}
}
