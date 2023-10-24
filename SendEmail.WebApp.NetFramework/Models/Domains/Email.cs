using System.ComponentModel.DataAnnotations;

namespace SendEmail.WebApp.NetFramework.Models.Domains
{
	public class Email
	{
        public int EmailId { get; set; }

		[Required]
        public string SenderName { get; set; }

		[Required]
		public string SenderEmail { get; set; }

		[Required]
		[Display(Name = "Odbiorca")]
		public string RecipientEmail { get; set; }

		[Required]
		[Display(Name = "Temat")]
		public string Subject { get; set; }

		[Display(Name = "Wiadomość")]
		public string Message { get; set; }
	}
}