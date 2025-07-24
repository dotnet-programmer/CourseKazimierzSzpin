using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.WebApp.Persistence.Repositories;

public class InvoiceRepository(IApplicationDbContext context)
{
	internal void Add(Invoice invoice)
	{
		invoice.CreatedDate = DateTime.Now;
		context.Invoices.Add(invoice);
		context.SaveChanges();
	}

	internal void AddPosition(InvoicePosition invoicePosition, string userId)
	{
		var invoice = context.Invoices.Single(x => x.Id == invoicePosition.InvoiceId && x.UserId == userId);
		context.InvoicePositions.Add(invoicePosition);
		context.SaveChanges();
	}

	internal void Delete(int invoiceId, string userId)
	{
		context.Invoices.Remove(context.Invoices.Single(x => x.Id == invoiceId && x.UserId == userId));
		context.SaveChanges();
	}

	internal void DeletePosition(int positionId, string userId)
	{
		var positionToDelete = context.InvoicePositions
			.Include(x => x.Invoice)
			.Single(x => x.Id == positionId && x.Invoice!.UserId == userId);
		context.InvoicePositions.Remove(positionToDelete);
		context.SaveChanges();
	}

	internal Invoice GetInvoice(int invoiceId, string userId)
		=> context.Invoices
			.Include(x => x.InvoicePositions)
			.ThenInclude(x => x.Product)
			.Include(x => x.MethodOfPayment)
			.Include(x => x.User)
			.ThenInclude(x => x!.Address)
			.Include(x => x.Client)
			.ThenInclude(x => x!.Address)
			.Single(x => x.Id == invoiceId && x.UserId == userId);

	internal InvoicePosition GetInvoicePosition(int invoicePositionId, string userId)
		=> context.InvoicePositions
			.Include(x => x.Invoice)
			.Single(x => x.Id == invoicePositionId && x.Invoice!.UserId == userId);

	internal List<Invoice> GetInvoices(string userId)
		=> context.Invoices
			.Include(x => x.Client)
			.Where(x => x.UserId == userId)
			.ToList();

	internal List<MethodOfPayment> GetMethodOfPayments()
		=> context.MethodOfPayments.ToList();

	internal void Update(Invoice invoice)
	{
		var invoiceToUpdate = context.Invoices.Single(x => x.Id == invoice.Id && x.UserId == invoice.UserId);
		invoiceToUpdate.ClientId = invoice.ClientId;
		invoiceToUpdate.Comments = invoice.Comments;
		invoiceToUpdate.MethodOfPaymentId = invoice.MethodOfPaymentId;
		invoiceToUpdate.PaymentDate = invoice.PaymentDate;
		invoiceToUpdate.Title = invoice.Title;
		context.SaveChanges();
	}

	internal decimal UpdateInvoiceValue(int invoiceId, string userId)
	{
		var invoice = context.Invoices
			.Include(x => x.InvoicePositions)
			.Single(x => x.Id == invoiceId && x.UserId == userId);
		invoice.Value = invoice.InvoicePositions.Sum(x => x.Value);
		context.SaveChanges();
		return invoice.Value;
	}

	internal void UpdatePosition(InvoicePosition invoicePosition, string userId)
	{
		var positionToUpdate = context.InvoicePositions
			.Include(x => x.Product)
			.Include(x => x.Invoice)
			.Single(x => x.Id == invoicePosition.Id && x.Invoice!.UserId == userId);
		positionToUpdate.Lp = invoicePosition.Lp;
		positionToUpdate.ProductId = invoicePosition.ProductId;
		positionToUpdate.Quantity = invoicePosition.Quantity;
		positionToUpdate.Value = invoicePosition.Product!.Value * invoicePosition.Value;
		context.SaveChanges();
	}
}