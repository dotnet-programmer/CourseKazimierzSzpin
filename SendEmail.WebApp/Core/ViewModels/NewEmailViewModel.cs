using System.ComponentModel.DataAnnotations;

namespace SendEmail.WebApp.Core.ViewModels;

public class NewEmailViewModel
{
	public int SettingsId { get; set; }

	[Required(ErrorMessage = "Wymagany jest adres e-mail odbiorcy.")]
	[Display(Name = "Odbiorca")]
	public string RecipientEmail { get; set; } = default!;

	[Required(ErrorMessage = "Wymagany jest temat wiadomości.")]
	[Display(Name = "Temat")]
	public string Subject { get; set; } = default!;

	[Display(Name = "Wiadomość")]
	public string? Message { get; set; }
}