using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisements.WebApp.Controllers;

[Authorize(Roles = "admin")]
public class CategoryController : Controller
{
	public IActionResult Categories()
	{
		return View();
	}
}
