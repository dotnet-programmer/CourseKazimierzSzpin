using Advertisements.WebApp.Data.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisements.WebApp.Controllers;

[Authorize]
public class AdvertisementController : Controller
{
	[AllowAnonymous]
	public IActionResult Advertisements()
	{
		AdvertisementsViewModel vm = new()
		{
			
		};
		return View();
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
