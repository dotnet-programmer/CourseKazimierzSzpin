using System.Diagnostics;
using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;
using InvoiceManager.WebApp.Core.ViewModels;
using InvoiceManager.WebApp.Persistence.Extensions;
using InvoiceManager.WebApp.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManager.WebApp.Controllers;

// INFO - metody kontrolera nazywa siê akcjami, odpowiadaj¹ za wyœwietlenie odpowiedniego widoku

[Authorize]
public class HomeController(IApplicationDbContext context) : Controller
{
	private readonly InvoiceRepository _invoiceRepository = new(context);
	private readonly ClientRepository _clientRepository = new(context);
	private readonly ProductRepository _productRepository = new(context);

	#region Actions

	// umo¿liwia wykonanie akcji niezalogowanym u¿ytkownikom
	[AllowAnonymous]
	public IActionResult About()
	{
		ViewBag.Message = "Your application description page.";

		// przekazanie danych jako model z kontrolera do widoku
		string test = "testy 123";

		ViewBag.MyNumber = test;

		return View((object)test);
	}

	[AllowAnonymous]
	public IActionResult Contact()
	{
		ViewBag.Message = "Your contact page.";

		// zmiana domyœlnego widoku zwraacnego przez kontroler - trzeba podaæ nazwê widoku który ma zostaæ wyœwietlony
		//return View("About");

		return View();
	}

	public IActionResult Index()
		=> View(_invoiceRepository.GetInvoices(GetUserId()));

	//jeœli id = 0 to dodawanie nowej faktury, a jak jest jakieœ id to bêdzie edycja
	public IActionResult Invoice(int invoiceId = 0)
	{
		var userId = GetUserId();
		Invoice invoice = (invoiceId == 0) ? GetNewInvoice(userId) : _invoiceRepository.GetInvoice(invoiceId, userId);
		EditInvoiceViewModel viewModel = PrepareEditInvoiceViewModel(invoice, userId);
		return View(viewModel);
	}

	// oznaczenie metody typu Post
	[HttpPost]
	// INFO - zabezpieczenie przed atakiem Cross-site request forgery (w skrócie CSRF lub XSRF)
	[ValidateAntiForgeryToken]
	// parametr u¿ywany przez formularz, który dopasowuje nazwy inputów do nazw w³aœciwoœci obiektu i przypisuje im odpowiednie wartoœci
	public IActionResult Invoice(Invoice invoice)
	{
		var userId = GetUserId();
		invoice.UserId = userId;

		// INFO - walidacja poprawnoœci wprowadzonych danych
		// ModelState.IsValid sprawdza poprawnoœæ pól na podstawie danych wpisanych w atrybutach modeli, np czy jest wymagany, jaka d³ugoœæ itp.
		if (!ModelState.IsValid)
		{
			EditInvoiceViewModel viewModel = PrepareEditInvoiceViewModel(invoice, userId);
			return View("Invoice", viewModel);
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
	{
		var userId = GetUserId();
		InvoicePosition invoicePosition = (invoicePositionId == 0) ? GetNewInvoicePosition(invoiceId, invoicePositionId) : _invoiceRepository.GetInvoicePosition(invoicePositionId, userId);
		EditInvoicePositionViewModel viewModel = PrepareEditInvoicePositionViewModel(invoicePosition);
		return View(viewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult InvoicePosition(InvoicePosition invoicePosition)
	{
		if (!ModelState.IsValid)
		{
			var vm = PrepareEditInvoicePositionViewModel(invoicePosition);
			return View("InvoicePosition", vm);
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

		// powrót do faktury, która by³a edytowana
		return RedirectToAction("Invoice", new { invoiceId = invoicePosition.InvoiceId });
	}

	// ta metoda zwraca JSON bo jest wywo³ywana przez Ajax
	[HttpPost]
	public IActionResult Delete(int invoiceId)
	{
		try
		{
			var userId = GetUserId();
			_invoiceRepository.Delete(invoiceId, userId);
		}
		catch (Exception ex)
		{
			// logowanie b³êdów
			return Json(new { Success = false, Message = ex.Message });
		}
		return Json(new { Success = true });
	}

	// ta metoda zwraca JSON bo jest wywo³ywana przez Ajax
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
			// logowanie b³êdów
			return Json(new { Success = false, Message = ex.Message });
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

	private EditInvoiceViewModel PrepareEditInvoiceViewModel(Invoice invoice, string userId)
		=> new EditInvoiceViewModel
		{
			Invoice = invoice,
			Heading = (invoice.Id == 0) ? "Dodawanie nowej faktury" : "Faktura",
			Clients = _clientRepository.GetClients(userId),
			MethodOfPayments = _invoiceRepository.GetMethodOfPayments()
		};

	private InvoicePosition GetNewInvoicePosition(int invoiceId, int invoicePositionId)
		=> new InvoicePosition
		{
			InvoiceId = invoiceId,
			Id = invoicePositionId,
		};

	private EditInvoicePositionViewModel PrepareEditInvoicePositionViewModel(InvoicePosition invoicePosition)
		=> new EditInvoicePositionViewModel
		{
			InvoicePosition = invoicePosition,
			Heading = (invoicePosition.Id == 0) ? "Dodawanie nowej pozycji" : "Pozycja",
			Products = _productRepository.GetProducts(),
		};

	#endregion Private methods

	#region Sesja

	// Sesje w ASP.NET Core s¹ domyœlnie ulotne, co oznacza, ¿e dane sesji s¹ tracone po zamkniêciu przegl¹darki.
	// Aby to zmieniæ, mo¿na u¿yæ pamiêci podrêcznej rozproszonej(np.Redis lub SQL Server).
	// Konfiguracja pamiêci podrêcznej:
	// Domyœlnie u¿ywana jest pamiêæ podrêczna IDistributedMemoryCache, ale mo¿na u¿yæ innych rozwi¹zañ, np.:
	// Redis(rozproszona pamiêæ podrêczna)
	// SQL Server(do przechowywania sesji w bazie danych)
	// Konfiguracja Redis wymaga dodania pakietu Microsoft.Extensions.Caching.StackExchangeRedis i skonfigurowania po³¹czenia:
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
		int newCookieValue = int.Parse(cookieValue) + 1;
		UpdateCookie(newCookieValue.ToString());

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

	// taka publiczna metoda mo¿e zostaæ wywo³ana jak ka¿da inna akcja
	// ¿eby to zablokowaæ, trzeba dodaæ atrybut [NonAction]
	public void PublicMethod()
	{
		// jakaœ logika...
	}

	#endregion Test Actions
}
