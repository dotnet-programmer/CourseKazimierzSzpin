using System.Collections.Generic;
using System.Linq;
using InvoiceManager.NetFramework.WebApp.Models.Domains;

namespace InvoiceManager.NetFramework.WebApp.Models.Repositories
{
	public class ProductRepository
	{
		internal Product GetProduct(int productId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return context.Products.Single(x => x.Id == productId);
			}
		}

		internal List<Product> GetProducts()
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return context.Products.ToList();
			}
		}
	}
}