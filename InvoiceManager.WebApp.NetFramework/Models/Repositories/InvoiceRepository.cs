using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InvoiceManager.WebApp.NetFramework.Models.Domains;

namespace InvoiceManager.WebApp.NetFramework.Models.Repositories
{
	public class InvoiceRepository
	{
		internal void Add(Invoice invoice)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				invoice.CreatedDate = DateTime.Now;
				context.Invoices.Add(invoice);
				context.SaveChanges();
			}
		}

		internal void AddPosition(InvoicePosition invoicePosition, string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				var invoice = context.Invoices
					.Single(x => x.Id == invoicePosition.InvoiceId && x.UserId == userId);

				context.InvoicePositions.Add(invoicePosition);

				context.SaveChanges();
			}
		}

		internal void Delete(int invoiceId, string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				var invoiceToDelete = context.Invoices
					.Single(x => x.Id == invoiceId && x.UserId == userId);

				context.Invoices.Remove(invoiceToDelete);

				context.SaveChanges();
			}
		}

		internal void DeletePosition(int positionId, string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				var positionToDelete = context.InvoicePositions
					.Include(x => x.Invoice)
					.Single(x => x.Id == positionId && x.Invoice.UserId == userId);

				context.InvoicePositions.Remove(positionToDelete);

				context.SaveChanges();
			}
		}

		internal Invoice GetInvoice(int invoiceId, string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return context.Invoices
					.Include(x => x.InvoicePositions)
					.Include(x => x.InvoicePositions.Select(y => y.Product))
					.Include(x => x.MethodOfPayment)
					.Include(x => x.User)
					.Include(x => x.User.Address)
					.Include(x => x.Client)
					.Include(x => x.Client.Address)
					.Single(x => x.Id == invoiceId && x.UserId == userId);
			}
		}

		internal InvoicePosition GetInvoicePosition(int invoicePositionId, string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return context.InvoicePositions
					.Include(x => x.Invoice)
					.Single(x => x.Id == invoicePositionId && x.Invoice.UserId == userId);
			}
		}

		internal List<Invoice> GetInvoices(string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return context.Invoices
					.Include(x => x.Client)
					.Where(x => x.UserId == userId)
					.ToList();
			}
		}

		internal List<MethodOfPayment> GetMethodOfPayments()
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return context.MethodOfPayments.ToList();
			}
		}

		internal void Update(Invoice invoice)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				var invoiceToUpdate = context.Invoices
					.Single(x => x.Id == invoice.Id && x.UserId == invoice.UserId);

				invoiceToUpdate.ClientId = invoice.ClientId;
				invoiceToUpdate.Comments = invoice.Comments;
				invoiceToUpdate.MethodOfPaymentId = invoice.MethodOfPaymentId;
				invoiceToUpdate.PaymentDate = invoice.PaymentDate;
				invoiceToUpdate.Title = invoice.Title;

				context.SaveChanges();
			}
		}

		internal decimal UpdateInvoiceValue(int invoiceId, string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				var invoice = context.Invoices
					.Include(x => x.InvoicePositions)
					.Single(x => x.Id == invoiceId && x.UserId == userId);

				invoice.Value = invoice.InvoicePositions.Sum(x => x.Value);

				context.SaveChanges();

				return invoice.Value;
			}
		}

		internal void UpdatePosition(InvoicePosition invoicePosition, string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				var positionToUpdate = context.InvoicePositions
					.Include(x => x.Product)
					.Include(x => x.Invoice)
					.Single(x => x.Id == invoicePosition.Id && x.Invoice.UserId == userId);

				positionToUpdate.Lp = invoicePosition.Lp;
				positionToUpdate.ProductId = invoicePosition.ProductId;
				positionToUpdate.Quantity = invoicePosition.Quantity;
				positionToUpdate.Value = invoicePosition.Product.Value * invoicePosition.Value;

				context.SaveChanges();
			}
		}
	}
}