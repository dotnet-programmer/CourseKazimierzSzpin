using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;
using InvoiceManager.WebApp.Persistence.Extensions;
using InvoiceManager.WebApp.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace InvoiceManager.WebApp.Controllers;

[Authorize]
public class PrintController(IApplicationDbContext context) : Controller
{
	private readonly InvoiceRepository _invoiceRepository = new(context);

	// Generowanie PDF za pomocą NuGet: Rotativa

	// kod testowy, do wyświetlenia w przeglądarce podglądu
	public IActionResult InvoiceTemplate(int id)
		=> View(_invoiceRepository.GetInvoice(id, User.GetUserId()));

	// pierwsza akcja, na podstawie przekazanego id konwertuje fakturę do tablicy bajtów
	public IActionResult InvoiceToPdf(int invoiceId)
	{
		// unikalny losowy ciąg znaków
		var handle = Guid.NewGuid().ToString();

		// sprawdzenie który użyszkodnik zalogowany
		var userId = User.GetUserId();

		// pobranie faktury z BD na podstawie przekazanego id faktury i id użyszkodnika
		var invoice = _invoiceRepository.GetInvoice(invoiceId, userId);

		// pobranie tablicy bajtów na podstawie przekazanego modelu
		// tablica bajtów z odpowiednim kluczem przechowana po stronie serwera za pomocą tymczasowego zapisu po stronie serwera - TempData
		TempData[handle] = GetPdfContent(invoice);

		// ta akcja jest wywoływana przez Ajax więc zwraca Json-a
		// zawiera unikalny klucz FileGuid oraz nazwę pliku
		return Json(new { FileGuid = handle, FileName = $@"Faktura_{invoice.Id}.pdf" });
	}

	public IActionResult DownloadInvoicePdf(string fileGuid, string fileName)
	{
		// na podstawie wygenerowanego wcześniej Guid oraz nazwy pliku sprawdzenie czy po stronie serwera znajduje się tymczasowy plik
		if (TempData[fileGuid] == null)
		{
			throw new Exception("Błąd przy próbie eksportu faktury do PDF.");
		}

		// jeśli jest plik w tymczasowym zapisie TempData to odczyt, rzutowanie na tablicę bajtów i zwrócenie gotowego pliku
		var data = TempData[fileGuid] as byte[];

		// parametry - tablica bajtów, jaki to jest plik, nazwa pliku
		return File(data, "application/pdf", fileName);
	}

	public IActionResult PrintInvoicePdf(int invoiceId)
	{
		var userId = User.GetUserId();
		var invoice = _invoiceRepository.GetInvoice(invoiceId, userId);
		return View("InvoiceTemplate", invoice);
	}

	private async Task<byte[]> GetPdfContent(Invoice invoice)
	{
		// użycie Rotativa, na podstawie widoku generowany jest PDF,
		// parametry wywołania to nazwa widoku i model do tego widoku
		var pdfResult = new ViewAsPdf(@"InvoiceTemplate", invoice)
		{
			PageSize = Size.A4,
			PageOrientation = Orientation.Portrait
		};
		return await pdfResult.BuildFile(ControllerContext);
	}
}