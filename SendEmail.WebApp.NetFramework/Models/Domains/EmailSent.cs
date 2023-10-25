using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SendEmail.WebApp.NetFramework.Models.Domains
{
	public class EmailSent
	{
		public int EmailSentId { get; set; }

		public string SenderName { get; set; }

		public string SenderEmail { get; set; }

		[Required]
		[Display(Name = "Odbiorca")]
		public string RecipientEmail { get; set; }

		[Required]
		[Display(Name = "Temat")]
		public string Subject { get; set; }

		[Display(Name = "Wiadomość")]
		public string Message { get; set; }

		public DateTime DateSent { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public string UserId { get; set; }

		public ApplicationUser User { get; set; }
	}
}