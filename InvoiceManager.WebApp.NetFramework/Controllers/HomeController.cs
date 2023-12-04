using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using InvoiceManager.WebApp.NetFramework.Models.Domains;
using InvoiceManager.WebApp.NetFramework.Models.Repositories;
using InvoiceManager.WebApp.NetFramework.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace InvoiceManager.WebApp.NetFramework.Controllers
{
	// INFO - metody kontrolera nazywa się akcjami, odpowiadają za wyświetlenie odpowiedniego widoku

	[Authorize]
	public class HomeController : Controller
	{
		private readonly InvoiceRepository _invoiceRepository = new InvoiceRepository();
		private readonly ClientRepository _clientRepository = new ClientRepository();
		private readonly ProductRepository _productRepository = new ProductRepository();

		[AllowAnonymous]
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			// przekazanie danych jako model z kontrolera do widoku
			string test = "testy 123";

			ViewBag.MyNumber = test;

			// INFO - Sesja
			// pobranie wartości sesji
			int sessionValue = GetSession();
			// zwiększenie wartości sesji
			sessionValue++;
			// aktualizacja wartości sesji
			UpdateSession(sessionValue);
			// wyświetlenie wartości sesji w widoku za pomocą ViewBag
			ViewBag.SessionValue = sessionValue;

			// INFO - Cookie
			// pobranie wartości cookie
			int cookieValue = GetCookie();
			// zwiększenie wartości cookie
			cookieValue++;
			// aktualizacja wartości cookie
			UpdateCookie(cookieValue);
			// wyświetlenie wartości cookie w widoku za pomocą ViewBag
			ViewBag.CookieValue = cookieValue;

			// INFO - Cache
			// pobranie wartości cache
			int cacheValue = GetCache();
			// zwiększenie wartości cache
			cacheValue++;
			// aktualizacja wartości cache
			UpdateCache(cacheValue);
			// wyświetlenie wartości cache w widoku za pomocą ViewBag
			ViewBag.CacheValue = cacheValue;

			return View((object)test);
		}

		private static int number = 0;

		[AllowAnonymous]
		// INFO - Cache
		[OutputCache(Duration = 10)]
		public ActionResult Contact()
		{
			number++;

			ViewBag.Message = "Your contact page. =>" + number;

			// zmiana domyślnego widoku zwraacnego przez kontroler - trzeba podać nazwę widoku który ma zostać wyświetlony
			//return View("About");

			return View();
		}

		public ActionResult Index() =>
			//var userId = User.Identity.GetUserId();
			//List<Invoice> invoices = _invoiceRepository.GetInvoices(userId);
			//return View(invoices);

			View(_invoiceRepository.GetInvoices(GetUserId()));

		//jeśli id = 0 to dodawanie nowej faktury, a jak jest jakieś id to będzie edycja
		public ActionResult Invoice(int invoiceId = 0)
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

		// parametr używany przez formularz, który dopasowuje nazwy inputów do nazw właściwości obiektu i przypisuje im odpowiednie wartości
		public ActionResult Invoice(Invoice invoice)
		{
			var userId = GetUserId();
			invoice.UserId = userId;

			// INFO - walidacja poprawności wprowadzonych danych
			// ModelState.IsValid sprawdza poprawność pól na podstawie danych wpisanych w atrybutach modeli, np czy jest wymagany, jaka długość itp.
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

		public ActionResult InvoicePosition(int invoiceId, int invoicePositionId = 0)
		{
			var userId = GetUserId();
			InvoicePosition invoicePosition = (invoicePositionId == 0) ? GetNewInvoicePosition(invoiceId, invoicePositionId) : _invoiceRepository.GetInvoicePosition(invoicePositionId, userId);
			EditInvoicePositionViewModel viewModel = PrepareEditInvoicePositionViewModel(invoicePosition);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult InvoicePosition(InvoicePosition invoicePosition)
		{
			if (!ModelState.IsValid)
			{
				var vm = PrepareEditInvoicePositionViewModel(invoicePosition);
				return View("InvoicePosition", vm);
			}

			Product product = _productRepository.GetProduct(invoicePosition.ProductId);
			invoicePosition.Value = invoicePosition.Quantity + product.Value;

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
			return RedirectToAction("Invoice", new { invoiceId = invoicePosition.InvoiceId });
		}

		// ta metoda zwraca JSON bo jest wywoływana przez Ajax
		[HttpPost]
		public ActionResult Delete(int invoiceId)
		{
			try
			{
				var userId = GetUserId();
				_invoiceRepository.Delete(invoiceId, userId);
			}
			catch (Exception ex)
			{
				// logowanie błędów
				return Json(new { Success = false, Message = ex.Message });
			}
			return Json(new { Success = true });
		}

		// ta metoda zwraca JSON bo jest wywoływana przez Ajax
		[HttpPost]
		public ActionResult DeletePosition(int positionId, int invoiceId)
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
				// logowanie błędów
				return Json(new { Success = false, Message = ex.Message });
			}
			return Json(new { Success = true, InvoiceValue = invoiceValue });
		}

		private string GetUserId() => User.Identity.GetUserId();

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

		#region Sesja

		// INFO - sesja
		// ta metoda będzie zawsze aktualizować sesję o wybranym kluczu o wartość podaną w argumencie
		// argument - tutaj jest to wartość o jaką będzie aktualizowana sesja
		private void UpdateSession(int i) =>
			// odwołanie do konkretnej sesji, w nawiasach kwadratowych trzeba podać klucz
			Session["nr"] = i;

		// ta metoda pobiera sesję
		private int GetSession() => Session["nr"] != null ? (int)Session["nr"] : 0;

		#endregion Sesja

		#region Cookies - ciasteczka

		// INFO - Cookies - ciasteczka
		// dodawanie nowego cookie albo aktualizacja
		private void UpdateCookie(int i)
		{
			// pierwszy argument to nazwa ciastka, drugi to wartość jaka ma być przypisana
			var cookie = new HttpCookie("nr", i.ToString())
			{
				// data wygaśnięcia ciastka
				Expires = DateTime.Now.AddDays(365)
			};

			// w odpowiedzi ustawienie ciastka
			Response.SetCookie(cookie);
		}

		// pobieranie ciastka
		private int GetCookie() =>
			// sprawdzenie czy cookie istnieje
			Request.Cookies["nr"] != null ? int.Parse(Request.Cookies["nr"].Value) : 0;

		#endregion Cookies - ciasteczka

		#region Cache

		// INFO - Cache
		private void UpdateCache(int i) => HttpRuntime.Cache["nr"] = i;

		private int GetCache() => HttpRuntime.Cache["nr"] != null ? (int)HttpRuntime.Cache["nr"] : 0;

		#endregion Cache

		#region Test Actions

		public ActionResult Test()
		{
			ViewBag.MyNumber = 666;
			return View("About");
		}

		public ActionResult Test1() => PartialView("About");

		public ActionResult Test2() => RedirectToAction("About", "Home");

		public ActionResult Test3()
		{
			Invoice invoice = new Invoice { Id = 3123, Title = "Test" };
			return Json(invoice, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Test4() => File("../Web.config", "text");

		public ActionResult Test5() => Content("<script>alert('ALERT!')</script>");

		#endregion Test Actions
	}
}