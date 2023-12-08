using System.ComponentModel.DataAnnotations;

namespace SendEmail.NetFramework.WebApp.Models.ViewModels
{
	public class ForgotViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}