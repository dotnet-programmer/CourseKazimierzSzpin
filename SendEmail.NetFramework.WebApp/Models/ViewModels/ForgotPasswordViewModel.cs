using System.ComponentModel.DataAnnotations;

namespace SendEmail.NetFramework.WebApp.Models.ViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}