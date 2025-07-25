using System.ComponentModel.DataAnnotations;

namespace SendEmail.WebApp.Core.Models.Domains;

public class Settings
{
	public int SettingsId { get; set; }

	[Required]
	[Display(Name = "Nazwa nadawcy")]
	public string SenderName { get; set; } = default!;

	[Required]
	[Display(Name = "Email nadawcy")]
	public string SenderEmail { get; set; } = default!;

	[Required]
	[Display(Name = "Hasło email nadawcy")]
	public string SenderEmailPassword { get; set; } = default!;

	[Required]
	[Display(Name = "Serwer poczty wychodzącej SMTP")]
	public string HostSmtp { get; set; } = default!;

	[Required]
	[Display(Name = "Port serwera poczty wychodzącej")]
	public int Port { get; set; }

	[Required]
	[Display(Name = "Tryb zabezpieczenia SSL")]
	public bool EnableSsl { get; set; }

	[Required]
	public string UserId { get; set; } = default!;

	public ApplicationUser? User { get; set; }
}