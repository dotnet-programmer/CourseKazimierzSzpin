using System.Collections.Generic;
using System.Linq;
using InvoiceManager.WebApp.NetFramework.Models.Domains;

namespace InvoiceManager.WebApp.NetFramework.Models.Repositories
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