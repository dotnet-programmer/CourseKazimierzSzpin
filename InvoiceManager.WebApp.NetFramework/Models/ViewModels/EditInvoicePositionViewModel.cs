using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceManager.WebApp.NetFramework.Models.Domains;

namespace InvoiceManager.WebApp.NetFramework.Models.ViewModels
{
	public class EditInvoicePositionViewModel
	{
        public InvoicePosition InvoicePosition { get; set; }
        public string Heading { get; set; }
        public List<Product> Products { get; set; }
    }
}