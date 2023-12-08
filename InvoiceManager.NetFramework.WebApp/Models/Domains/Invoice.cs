using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManager.NetFramework.WebApp.Models.Domains
{
	public class Invoice
	{
		public int Id { get; set; }

		//INFO - walidacja poprawności wprowadzonych danych - własny error message
		[Required(ErrorMessage = "Pole Tytuł jest wymagane")]
		[Display(Name = "Tytuł")]
		public string Title { get; set; }

		[Display(Name = "Wartość")]
		public decimal Value { get; set; }

		[Display(Name = "Termin płatności")]
		public DateTime PaymentDate { get; set; }

		[Display(Name = "Data utworzenia")]
		public DateTime CreatedDate { get; set; }

		[Display(Name = "Uwagi")]
		public string Comments { get; set; }

		[Display(Name = "Sposób płatności")]
		public int MethodOfPaymentId { get; set; }

		public MethodOfPayment MethodOfPayment { get; set; }

		[Display(Name = "Klient")]
		public int ClientId { get; set; }

		public Client Client { get; set; }

		[Required]
		// jawna definicja klucza obcego - zrobione tak ponieważ UserId jest typu string i EF nie utworzyłby go automatycznie
		[ForeignKey(nameof(User))]
		public string UserId { get; set; }

		// ApplicationUser - klasa użytkownika stworzona przez szablon aplikacji
		public ApplicationUser User { get; set; }

		public ICollection<InvoicePosition> InvoicePositions { get; set; } = new Collection<InvoicePosition>();
	}
}