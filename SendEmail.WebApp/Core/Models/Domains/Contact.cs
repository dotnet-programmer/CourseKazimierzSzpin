using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SendEmail.WebApp.Core.Models.Domains;

public class Contact
{
	public int ContactId { get; set; }

	[Required]
	public string Email { get; set; } = default!;

	[Display(Name = "Nazwa")]
	public string? Name { get; set; }

	[Required]
	public string UserId { get; set; } = default!;

	public ApplicationUser? User { get; set; }
}