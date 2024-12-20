﻿using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.NetFramework.WebApp.Models.ViewModels
{
	public class VerifyPhoneNumberViewModel
	{
		[Required]
		[Display(Name = "Code")]
		public string Code { get; set; }

		[Required]
		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
	}
}