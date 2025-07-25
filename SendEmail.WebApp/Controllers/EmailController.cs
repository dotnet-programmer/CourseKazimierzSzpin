using Microsoft.AspNetCore.Mvc;

namespace SendEmail.WebApp.Controllers;
public class EmailController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult NewEmail()
	{
		return View();
	}

	public IActionResult Contacts()
	{
		return View();
	}

	public IActionResult History()
	{
		return View();
	}

	public IActionResult Settings()
	{
		return View();
	}
}