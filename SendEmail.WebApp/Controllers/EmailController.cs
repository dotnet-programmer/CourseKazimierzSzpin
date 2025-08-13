using Microsoft.AspNetCore.Mvc;
using SendEmail.WebApp.Core.Services;
using SendEmail.WebApp.Persistence.Extensions;

namespace SendEmail.WebApp.Controllers;

public class EmailController(IEmailService emailService) : Controller
{
	//public IActionResult Index()
	//{
	//	return View();
	//}

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

	[HttpPost]
	public IActionResult DeleteEmail(int id)
	{
		//try
		//{
		//	emailService.DeleteEmail(id, User.GetUserId());
		//}
		//catch (Exception ex)
		//{
		//	return Json(new { success = false, message = ex.Message });
		//}

		return Json(new { success = true });
	}
}