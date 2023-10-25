using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SendEmail.WebApp.NetFramework.Models.Domains
{
	public class Contact
	{
		public int ContactId { get; set; }
		public string Email { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public string UserId { get; set; }

		public ApplicationUser User { get; set; }
	}
}