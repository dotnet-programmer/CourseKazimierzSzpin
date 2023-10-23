using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceManager.WebApp.NetFramework.Models.Domains;

namespace InvoiceManager.WebApp.NetFramework.Models.ViewModels
{
	public class EditInvoiceViewModel
	{
        public Invoice Invoice { get; set; }
        public List<Client> Clients { get; set; }
        public List<MethodOfPayment> MethodOfPayments { get; set; }
        public string Heading { get; set; }
    }
}