using System.ComponentModel.DataAnnotations;

namespace SendEmail.WebApp.Core.Models.Domains;

public class EmailSent
{
	public int EmailSentId { get; set; }

	[Required]
	public string SenderName { get; set; } = default!;

	[Required]
	public string SenderEmail { get; set; } = default!;

	[Required]
	[Display(Name = "Odbiorca")]
	public string RecipientEmail { get; set; } = default!;

	[Required]
	[Display(Name = "Temat")]
	public string Subject { get; set; } = default!;

	[Display(Name = "Wiadomość")]
	public string? Message { get; set; }

	[Required]
	public DateTime DateSent { get; set; }

	[Required]
	public string UserId { get; set; } = default!;

	public ApplicationUser? User { get; set; }
}