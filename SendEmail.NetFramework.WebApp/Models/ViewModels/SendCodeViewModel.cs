﻿using System.Collections.Generic;

namespace SendEmail.NetFramework.WebApp.Models.ViewModels
{
	public class SendCodeViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
		public string ReturnUrl { get; set; }
		public bool RememberMe { get; set; }
	}
}