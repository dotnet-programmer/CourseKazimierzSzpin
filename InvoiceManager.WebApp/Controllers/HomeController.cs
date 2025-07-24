using System.Diagnostics;
using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;
using InvoiceManager.WebApp.Core.ViewModels;
using InvoiceManager.WebApp.Persistence.Extensions;
using InvoiceManager.WebApp.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManager.WebApp.Controllers;

[Authorize]
public class HomeController(IApplicationDbContext context) : Controller
{
	private readonly InvoiceRepository _invoiceRepository = new(context);
	private readonly ClientRepository _clientRepository = new(context);
	private readonly ProductRepository _productRepository = new(context);

	#region Actions

	[AllowAnonymous]
	public IActionResult About()
	{
		ViewBag.Message = "Your application description page.";
		string test = "testy 123";
		ViewBag.MyNumber = test;
		return View((object)test);
	}

	[AllowAnonymous]
	public IActionResult Contact()
	{
		ViewBag.Message = "Your contact page.";
		return View();
	}

	public IActionResult Index()
		=> View(_invoiceRepository.GetInvoices(GetUserId()));

	public IActionResult Invoice(int invoiceId = 0)
		=> View(GetEditInvoiceViewModel(invoiceId));

	[HttpPost]
	// INFO - zabezpieczenie przed atakiem Cross-site request forgery (w skr�cie CSRF lub XSRF)
	[ValidateAntiForgeryToken]
	public IActionResult Invoice(Invoice invoice)
	{
		var userId = GetUserId();
		invoice.UserId = userId;

		// INFO - walidacja poprawno�ci wprowadzonych danych
		// ModelState.IsValid sprawdza poprawno�� p�l na podstawie danych wpisanych w atrybutach modeli, np czy jest wymagany, jaka d�ugo�� itp.
		if (!ModelState.IsValid)
		{
			return View("Invoice", GetEditInvoiceViewModel(invoice, userId));
		}

		if (invoice.Id == 0)
		{
			_invoiceRepository.Add(invoice);
		}
		else
		{
			_invoiceRepository.Update(invoice);
		}
		return RedirectToAction("Index");
	}

	public IActionResult InvoicePosition(int invoiceId, int invoicePositionId = 0)
		=> View(GetEditInvoicePositionViewModel(invoiceId, invoicePositionId));

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult InvoicePosition(InvoicePosition invoicePosition)
	{
		if (!ModelState.IsValid)
		{
			return View("InvoicePosition", GetEditInvoicePositionViewModel(invoicePosition));
		}

		Product product = _productRepository.GetProduct(invoicePosition.ProductId);
		invoicePosition.Value = invoicePosition.Quantity * product.Value;

		var userId = GetUserId();
		if (invoicePosition.Id == 0)
		{
			_invoiceRepository.AddPosition(invoicePosition, userId);
		}
		else
		{
			_invoiceRepository.UpdatePosition(invoicePosition, userId);
		}
		_invoiceRepository.UpdateInvoiceValue(invoicePosition.InvoiceId, userId);

		// powr�t do faktury, kt�ra by�a edytowana
		return RedirectToAction("Invoice", new { invoiceId = invoicePosition.InvoiceId });
	}

	// ta metoda zwraca JSON bo jest wywo�ywana przez Ajax
	[HttpPost]
	public IActionResult Delete(int invoiceId)
	{
		try
		{
			_invoiceRepository.Delete(invoiceId, GetUserId());
		}
		catch (Exception ex)
		{
			// logowanie b��d�w
			return Json(new { Success = false, ex.Message });
		}
		return Json(new { Success = true });
	}

	// ta metoda zwraca JSON bo jest wywo�ywana przez Ajax
	[HttpPost]
	public IActionResult DeletePosition(int positionId, int invoiceId)
	{
		decimal invoiceValue;
		try
		{
			var userId = GetUserId();
			_invoiceRepository.DeletePosition(positionId, userId);
			invoiceValue = _invoiceRepository.UpdateInvoiceValue(invoiceId, userId);
		}
		catch (Exception ex)
		{
			// logowanie b��d�w
			return Json(new { Success = false, ex.Message });
		}
		return Json(new { Success = true, InvoiceValue = invoiceValue });
	}

	[AllowAnonymous]
	public IActionResult Privacy()
		=> View();

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
		=> View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

	#endregion Actions

	#region Private methods

	private string GetUserId()
		=> User.GetUserId();

	private Invoice GetNewInvoice(string userId)
		=> new Invoice
		{
			UserId = userId,
			CreatedDate = DateTime.Now,
			PaymentDate = DateTime.Now.AddDays(7)
		};

	private EditInvoiceViewModel GetEditInvoiceViewModel(int invoiceId)
	{
		var userId = GetUserId();
		Invoice invoice = (invoiceId == 0) ? GetNewInvoice(userId) : _invoiceRepository.GetInvoice(invoiceId, userId);
		return GetEditInvoiceViewModel(invoice, userId);
	}

	private EditInvoiceViewModel GetEditInvoiceViewModel(Invoice invoice, string userId)
		=> new EditInvoiceViewModel
		{
			Invoice = invoice,
			Heading = (invoice.Id == 0) ? "Dodawanie nowej faktury" : "Faktura",
			Clients = _clientRepository.GetClients(userId),
			MethodOfPayments = _invoiceRepository.GetMethodOfPayments()
		};

	private EditInvoicePositionViewModel GetEditInvoicePositionViewModel(int invoiceId, int invoicePositionId)
	{
		InvoicePosition invoicePosition = (invoicePositionId == 0) ? GetNewInvoicePosition(invoiceId, invoicePositionId) : _invoiceRepository.GetInvoicePosition(invoicePositionId, GetUserId());
		return GetEditInvoicePositionViewModel(invoicePosition);
	}

	private EditInvoicePositionViewModel GetEditInvoicePositionViewModel(InvoicePosition invoicePosition)
		=> new EditInvoicePositionViewModel
		{
			InvoicePosition = invoicePosition,
			Heading = (invoicePosition.Id == 0) ? "Dodawanie nowej pozycji" : "Pozycja",
			Products = _productRepository.GetProducts(),
		};

	private InvoicePosition GetNewInvoicePosition(int invoiceId, int invoicePositionId)
		=> new InvoicePosition
		{
			InvoiceId = invoiceId,
			Id = invoicePositionId,
		};

	#endregion Private methods

	#region Sesja

	// Sesje w ASP.NET Core s� domy�lnie ulotne, co oznacza, �e dane sesji s� tracone po zamkni�ciu przegl�darki.
	// Aby to zmieni�, mo�na u�y� pami�ci podr�cznej rozproszonej(np.Redis lub SQL Server).
	// Konfiguracja pami�ci podr�cznej:
	// Domy�lnie u�ywana jest pami�� podr�czna IDistributedMemoryCache, ale mo�na u�y� innych rozwi�za�, np.:
	// Redis(rozproszona pami�� podr�czna)
	// SQL Server(do przechowywania sesji w bazie danych)
	// Konfiguracja Redis wymaga dodania pakietu Microsoft.Extensions.Caching.StackExchangeRedis i skonfigurowania po��czenia:
	// services.AddStackExchangeRedisCache(options =>
	//    {
	//        options.Configuration = "localhost"; // Adres serwera Redis
	//        options.InstanceName = "SampleInstance";
	//    });

	private int? GetSession()
		=> HttpContext.Session.GetInt32("MySessionKey");

	private void UpdateSession(int i)
		=> HttpContext.Session.SetInt32("MySessionKey", i);

	#endregion Sesja

	#region Cookies - ciasteczka

	private string? GetCookie()
		=> HttpContext.Request.Cookies["MyCookieKey"];

	private void UpdateCookie(string cookieValue)
		=> HttpContext.Response.Cookies.Append("MyCookieKey", cookieValue, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7) });

	private void DeleteCookie(string myCookieKey)
		=> HttpContext.Response.Cookies.Delete(myCookieKey);

	#endregion Cookies - ciasteczka

	#region Test Actions

	public IActionResult SessionCookiesCache()
	{
		// INFO - Sesja
		int? sessionValue = GetSession();
		int newSessionValue = sessionValue.GetValueOrDefault() + 1;
		UpdateSession(newSessionValue);

		// INFO - Cookie
		string? cookieValue = GetCookie();
		if (!string.IsNullOrWhiteSpace(cookieValue))
		{
			int newCookieValue = int.Parse(cookieValue) + 1;
			UpdateCookie(newCookieValue.ToString());
		}

		return View("About");
	}

	public IActionResult Test()
	{
		ViewBag.MyNumber = 666;
		return View("About");
	}

	public IActionResult Test1()
		=> PartialView("About");

	public IActionResult Test2()
		=> RedirectToAction("About", "Home");

	public IActionResult Test3()
	{
		Invoice invoice = new Invoice { Id = 3123, Title = "Test" };
		return Json(invoice);
	}

	public IActionResult Test4()
		=> File("../Web.config", "text");

	public IActionResult Test5()
		=> Content("<script>alert('ALERT!')</script>");

	// taka publiczna metoda mo�e zosta� wywo�ana jak ka�da inna akcja
	// �eby to zablokowa�, trzeba doda� atrybut [NonAction]
	public void PublicMethod()
	{
		// jaka� logika...
	}

	#endregion Test Actions
}
