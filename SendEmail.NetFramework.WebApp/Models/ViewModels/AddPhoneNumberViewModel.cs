using System.ComponentModel.DataAnnotations;

namespace SendEmail.NetFramework.WebApp.Models.ViewModels
{
	public class AddPhoneNumberViewModel
	{
		[Required]
		[Phone]
		[Display(Name = "Phone Number")]
		public string Number { get; set; }
	}
}