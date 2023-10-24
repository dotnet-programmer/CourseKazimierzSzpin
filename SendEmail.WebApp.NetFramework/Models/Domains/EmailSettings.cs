using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SendEmail.WebApp.NetFramework.Models.Domains
{
	public class EmailSettings
	{
        public int EmailSettingsId { get; set; }

		[Required]
		[Display(Name = "Nazwa nadawcy")]
		public string SenderName { get; set; }

		[Required]
		[Display(Name = "Email nadawcy")]
		public string SenderEmail { get; set; }

		[Required]
		[Display(Name = "Hasło email nadawcy")]
		public string SenderEmailPassword { get; set; }

		[Required]
		[Display(Name = "Host SMTP")]
		public string HostSmtp { get; set; }

		[Display(Name = "SSL")]
		public bool EnableSsl { get; set; }
		
		public int Port { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}